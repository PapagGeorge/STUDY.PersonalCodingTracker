using Application.Interfaces;
using Domain.Entities;
using System.Collections;
using System.Net;
using Application.Constans;


namespace Application
{
    public class Application : IApplication
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IRentService _rentService;

        public Application(IBookRepository bookRepository, IMemberRepository memberRepository, IRentService rentService)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _rentService = rentService;
        }


        public void AddNewBook(Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.BookTitle))
                {
                    throw new Exception("Book Title cannot be null or Empty");
                    
                }
    
                if (string.IsNullOrEmpty(book.Author))
                {
                    throw new Exception("Author cannot be null or Empty");
                    
                }
                if (!(book.Inventory is int))
                {
                    throw new ArgumentException("Inventory must be an integer value");
                    
                }
                if (!(book.RentedCount is int))
                {
                    throw new ArgumentException("Rented Count must be an integer value");
                    
                }
                if(book.Inventory > 0)
                {
                    book.IsAvailable = true;
                }
                if (book.Inventory <= 0)
                {
                    book.IsAvailable = false;
                }

                book.isDeleted = false;
                _bookRepository.AddBook(book);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while adding new book. {ex.Message}");
            }
        }

        public void CreateNewMember(Member member)
        {
            try
            {
                if (string.IsNullOrEmpty(member.Name))
                {
                    throw new Exception("Member name cannot be null or Empty");

                }

                if (string.IsNullOrEmpty(member.Surname))
                {
                    throw new Exception("Member surname cannot be null or Empty");

                }
                if (string.IsNullOrEmpty(member.MobilePhone))
                {
                    throw new Exception("Member mobile phone cannot be null or Empty");

                }
                member.RentedBooks = 0;
                member.CanRent = true;
                member.isDeleted = false;

                _memberRepository.AddMember(member);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new member. {ex.Message}");
            }
        }

        public void DeleteBook(int bookId)
        {
            try
            {
                if (!_bookRepository.BookExists(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} does not exist.");
                }
                if (_bookRepository.BookIsOwed(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} is owed by members. It can only be deleted after returned.");
                }
                if (_bookRepository.isBookDeleted(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} is already deleted.");
                }
                _bookRepository.SoftDeleteBook(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while deleting book with Id: {bookId}. {ex.Message}");
            }

        }

        public void DeleteMember(int memberId)
        {
            try
            {
               
                if (!_memberRepository.MemberExists(memberId))
                {
                    throw new Exception($"Member with Id: {memberId} does not exist.");
                }
                if(_memberRepository.isMemberDeleted(memberId))
                {
                    throw new Exception($"Member with Id: {memberId} is already deleted.");
                }
                _memberRepository.SoftDeleteMember(memberId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while deleting member with Id: {memberId}. {ex.Message}");
            }
        }

        public void RentBook(int bookId, int memberId)
        {
            try
            {
                var booksOwedByMember = _memberRepository.BooksOwedByMember(memberId).ToList();


                if(booksOwedByMember.Any(book => book.BookId == bookId))
                {
                    throw new Exception($"Book with Id: {bookId} has allready been rented to member with Id: {memberId}.");
                }
                if(!_memberRepository.MemberExists(memberId))
                {
                    throw new Exception($"Member with Id: {memberId} does not exist.");
                }
                if (!_bookRepository.BookExists(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} does not exist.");
                }
                if (!_bookRepository.IsBookAvailable(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} is not available.");
                }
                if (_memberRepository.isMemberDeleted(memberId))
                {
                    throw new Exception($"Member with Id: {memberId} is deleted.");
                }
                if (_bookRepository.isBookDeleted(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} is deleted.");
                }
                if (!_memberRepository.CanMemberRentBooks(memberId))
                {
                    throw new Exception($"Member with Id: {memberId} cannot rent more books.");
                }

                _rentService.RentBook(memberId, bookId);
                _bookRepository.DecreaseBookAvailability(bookId, 1);

                if(_memberRepository.BooksOwedByMemberCount(memberId) >= Limits.maxBooksRentLimit)
                {
                    _memberRepository.RemoveMemberRentability(memberId);
                }

                if(_bookRepository.NumberOfBooksAvailable(bookId) <= 0)
                {
                    _bookRepository.MakeBookUnavailable(bookId);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while renting Book with Id: {bookId}" +
                    $"to member with Id {memberId}. {ex.Message}");
            }
        }

        public void ReturnBook(int bookId, int memberId)
        {
            try
            {
                if (!_memberRepository.MemberExists(memberId))
                {
                    throw new Exception($"Member with Id: {memberId} does not exist.");
                }
                if (!_bookRepository.BookExists(bookId))
                {
                    throw new Exception($"Book with Id: {bookId} does not exist.");
                }

                var booksOwedByMember = _memberRepository.BooksOwedByMember(memberId).ToList();

                if (!booksOwedByMember.Any(book => book.BookId == bookId))
                {
                    throw new Exception($"Book with Id: {bookId} is not rented to member with Id: {memberId}.");
                }

                _rentService.ReturnBook(memberId, bookId);
                _bookRepository.IncreaseBookAvailability(bookId, 1);


                if (_memberRepository.BooksOwedByMemberCount(memberId) < Limits.maxBooksRentLimit)
                {
                    _memberRepository.RestoreMemberRentability(memberId);
                }

                if (_bookRepository.NumberOfBooksAvailable(bookId) > 0)
                {
                    _bookRepository.MakeBookAvailable(bookId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while returning Book with Id: {bookId}" +
                    $"from member with Id {memberId}. {ex.Message}");
            }

        }

        public IEnumerable ShowAllBooks()
        {
            try
            {
                var allBooks = _bookRepository.GetAllActiveAvailableBooks();
                return allBooks;

            }
            catch (Exception ex )
            {
                throw new Exception("An error occured while trying to get all books list");
            }
        }

        public IEnumerable ShowAllMembers()
        {
            try
            {
                var allMembers = _memberRepository.GetAllActiveMembers();
                return allMembers;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while trying to get all members list");
            }
        }
    }
}
