using Domain.Entities;
using System.Collections;

namespace Application.Interfaces
{
    public interface IApplication
    {
        void RentBook(int bookId, int memberId);
        void ReturnBook(int bookId, int memberId);
        IEnumerable ShowAllBooks(); //--------------------
        IEnumerable ShowAllMembers();
        void CreateNewMember(Member member);
        void DeleteMember(int memberId);
        void AddNewBook(Book book); //----------------
        void DeleteBook(int bookId); //----------------

    }
}
