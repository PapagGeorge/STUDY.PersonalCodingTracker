using Application.Interfaces;
using Domain.Entities;
using Application.Constatnts;

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
                _memberRepository.RemoveMemberRentability(memberId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while deleting member with Id: {memberId}. {ex.Message}");
            }
        }

        public void RentBook(RentBookRequest rentBookRequest)
        {
            try
            {
                var booksOwedByMember = _memberRepository.BooksOwedByMember(rentBookRequest.MemberId).ToList();


                if (booksOwedByMember.Any(books => books.BookId == rentBookRequest.BookId))
                {
                    throw new Exception($"Book with Id: {rentBookRequest.BookId} has allready been rented to member with Id: {rentBookRequest.MemberId}.");
                }
                if (!_memberRepository.MemberExists(rentBookRequest.MemberId))
                {
                    throw new Exception($"Member with Id: {rentBookRequest.MemberId} does not exist.");
                }
                if (!_bookRepository.BookExists(rentBookRequest.BookId))
                {
                    throw new Exception($"Book with Id: {rentBookRequest.BookId} does not exist.");
                }
                if (!_bookRepository.IsBookAvailable(rentBookRequest.BookId))
                {
                    throw new Exception($"Book with Id: {rentBookRequest.BookId} is not available.");
                }
                if (_memberRepository.isMemberDeleted(rentBookRequest.MemberId))
                {
                    throw new Exception($"Member with Id: {rentBookRequest.MemberId} is deleted.");
                }
                if (_bookRepository.isBookDeleted(rentBookRequest.BookId))
                {
                    throw new Exception($"Book with Id: {rentBookRequest.BookId} is deleted.");
                }
                if (!_memberRepository.CanMemberRentBooks(rentBookRequest.MemberId))
                {
                    throw new Exception($"Member with Id: {rentBookRequest.MemberId} cannot rent more books.");
                }

                _rentService.RentBook(rentBookRequest);
                _bookRepository.DecreaseBookAvailability(rentBookRequest.BookId, 1);

                if(_memberRepository.BooksOwedByMemberCount(rentBookRequest.MemberId) >= Limits.maxBookRentLimit)
                {
                    _memberRepository.RemoveMemberRentability(rentBookRequest.MemberId);
                }

                if(_bookRepository.NumberOfBooksAvailable(rentBookRequest.BookId) <= 0)
                {
                    _bookRepository.MakeBookUnavailable(rentBookRequest.BookId);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while renting Book with Id: {rentBookRequest.BookId} " +
                    $"to member with Id {rentBookRequest.MemberId}. {ex.Message} {ex.StackTrace}");
            }
        }

        public void ReturnBook(ReturnBookRequest returnBookRequest)
        {
            try
            {
                if (!_memberRepository.MemberExists(returnBookRequest.MemberId))
                {
                    throw new Exception($"Member with Id: {returnBookRequest.MemberId} does not exist.");
                }
                if (!_bookRepository.BookExists(returnBookRequest.BookId))
                {
                    throw new Exception($"Book with Id: {returnBookRequest.BookId} does not exist.");
                }

                var booksOwedByMember = _memberRepository.BooksOwedByMember(returnBookRequest.MemberId).ToList();

                if (!booksOwedByMember.Any(book => book.BookId == returnBookRequest.BookId))
                {
                    throw new Exception($"Book with Id: {returnBookRequest.BookId} is not rented to member with Id: {returnBookRequest.MemberId}.");
                }

                _rentService.ReturnBook(returnBookRequest);
                _bookRepository.IncreaseBookAvailability(returnBookRequest.BookId, 1);


                if (_memberRepository.BooksOwedByMemberCount(returnBookRequest.MemberId) < Limits.maxBookRentLimit)
                {
                    _memberRepository.RestoreMemberRentability(returnBookRequest.MemberId);
                }

                if (_bookRepository.NumberOfBooksAvailable(returnBookRequest.BookId) > 0)
                {
                    _bookRepository.MakeBookAvailable(returnBookRequest.BookId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while returning Book with Id: {returnBookRequest.BookId} " +
                    $"from member with Id {returnBookRequest.MemberId}. {ex.Message} {ex.StackTrace}");
            }

        }

        public IEnumerable<Book> ShowAllBooks()
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

        public IEnumerable<Member> ShowAllMembers()
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
