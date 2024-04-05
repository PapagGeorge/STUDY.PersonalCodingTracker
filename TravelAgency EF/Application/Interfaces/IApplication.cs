namespace Application.Interfaces
{
    public interface IApplication
    {
        void BookTrip(long customerId, long transportationId, long accommodationId, long serviceId, int daysOfVisit);
        void TopChoicesByCustomers();
        void PayInvoice(long customerId, decimal amount, long invoiceId);
        void ShowCustomerInvoices(long customerId);
        void ShowTripChoices(int userChoice, long destionationId);
        void ShowAllDestinations();
    }
}
