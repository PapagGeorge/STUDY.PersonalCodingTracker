using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplication
    {
        void RentBook(RentBookRequest rentBookRequest);
        void ReturnBook(ReturnBookRequest returnBookRequest);
        IEnumerable <Book> ShowAllBooks();
        IEnumerable<Member> ShowAllMembers();
        void CreateNewMember(Member member);
        void DeleteMember(int memberId);
        void AddNewBook(Book book);
        void DeleteBook(int bookId);

    }
}
