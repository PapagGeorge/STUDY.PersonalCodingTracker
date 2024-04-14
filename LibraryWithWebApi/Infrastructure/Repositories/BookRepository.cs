﻿using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
            
        }

        public void AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to add new book. {ex.Message}");
            }
        }

        public bool BookExists(int bookId)
        {
            try
            {
                return _context.Books.Any(book => book.BookId == bookId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check if book with Id: {bookId} exists. {ex.Message}");
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                var result = _context.Books;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get all books. {ex.Message}");
            }
        }

        public bool IsBookAvailable(int bookId)
        {
            try
            {
                var bookToCheck = _context.Books.FirstOrDefault(book => book.BookId == bookId);

                if(bookToCheck.IsAvailable == true && bookToCheck != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check availability of book with Id: {bookId}.  {ex.Message}");
            }
        }

        public void SoftDeleteBook(int bookId)
        {
            try
            {
                
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);
                if (book != null)
                {
                    book.isDeleted = true;
                    _context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to delete book.  {ex.Message}");
            }
        }
    }
}
