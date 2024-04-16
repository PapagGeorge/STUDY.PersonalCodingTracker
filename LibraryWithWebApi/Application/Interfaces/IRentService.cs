using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRentService
    {
        void RentBook(RentBookRequest rentBookRequest);
        void ReturnBook(ReturnBookRequest returnBookRequest);

    }
}
