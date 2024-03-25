namespace LibraryApplication.Interfaces
{
    public interface IApplication
    {
        void SearchBook(string userSearchText);
        void AvailableBooks();
        void AllBooks();
        void RentBook(string userIsbn, int userId);
        void ReturnBook(string userIsbn, int userId);
    }
}
