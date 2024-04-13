namespace Application.Interfaces
{
    public interface IRentService
    {
        void RentBook(int memberId, int bookId);
        void ReturnBook(int memberId, int bookId);

    }
}
