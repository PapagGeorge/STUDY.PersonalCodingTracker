using Application.Interfaces;
using Domain.Entities;



namespace Application.Services
{
    public class Application : IApplication
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITripRepository _tripRepository;

        public Application(IAnalyticsRepository analyticsRepository, ICustomerRepository customerRepository, ITripRepository tripRepository)
        {
            _analyticsRepository = analyticsRepository;
            _customerRepository = customerRepository;
            _tripRepository = tripRepository;
        }

        public void ShowAllDestinations()
        {
            
            List<Destination> destinations = _tripRepository.ShowAllDestinations().ToList();

            if (destinations.Any())

            {
                foreach (Destination dest in destinations)
                {
                    Console.WriteLine($"Destination ID:{dest.DestinationId}   || Country:{dest.Country}   || City:{dest.City}");
                }
            }

            else
            {
                Console.WriteLine("Destination List is empty. Please try again later.");
            }
        }
        public void ShowTripChoices(long destionationId)
        {
            if (_tripRepository.DestinationExists(destionationId))
            {
                Console.WriteLine("\n\nBased on the destination you chose there are the following travelling choices at the moment: ");

                Console.WriteLine("\n\n\n-------Transportation-------");
                List<Transportation> trasportationList = _tripRepository.TransporationByDestination(destionationId).ToList();

                if (!trasportationList.Any())
                {
                    Console.WriteLine("Unfortunatelly there is no available transportation for the destination you chose at the momonet.");
                }

                else
                {
                    foreach (Transportation transportation in trasportationList)
                    {
                        Console.WriteLine("\n\n---------------");
                        Console.WriteLine($"Mode of Transportation: {transportation.TransportationMode}");
                        Console.WriteLine($"Price: {transportation.Price}");
                        Console.WriteLine($"Availability: {transportation.Availability}");
                        Console.WriteLine("---------------");
                    }
                }
                
                
                Console.WriteLine("\n\n\n-------Accommodation-------");
                List<Accommodation> accommodationList = _tripRepository.AccommodationByDestination(destionationId).ToList();

                if (!accommodationList.Any())
                {
                    Console.WriteLine("Unfortunatelly there is no available accommodation for the destination you chose at the momonet.");
                }
                else
                {
                    foreach (Accommodation accommodation in accommodationList)
                    {
                        Console.WriteLine("\n\n---------------");
                        Console.WriteLine($"Hotel: {accommodation.HotelName}");
                        Console.WriteLine($"Star Rating: {accommodation.StarRating}");
                        Console.WriteLine($"Price Per Person Per Day: {accommodation.PricePerPersonPerDay}");
                        Console.WriteLine($"Availability: {accommodation.Availability}");
                        Console.WriteLine("---------------");
                    }
                }


                Console.WriteLine("\n\n\n-------Additional Services-------");

                List<Service> serviceList = _tripRepository.ServicesByDestination(destionationId).ToList();

                if (!serviceList.Any())
                {
                    Console.WriteLine("Unfortunatelly there is no available additional service for the destination you chose at the momonet.");
                }

                else
                {
                    foreach (Service service in serviceList)
                    {
                        Console.WriteLine("\n\n---------------");
                        Console.WriteLine($"Service Description: {service.ServiceName}");
                        Console.WriteLine($"Price: {service.Price}");
                        Console.WriteLine($"Availability: {service.Availability}");
                        Console.WriteLine("---------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("The Destination Id you entered doess not exist.");
            }
        }
        
        public void BookTrip(long customerId, long transportationId, long accommodationId, long serviceId, int daysOfVisit)
        {
            try
            {
                
                    if (!_customerRepository.CustomerExists(customerId))
                    {
                        Console.WriteLine("The customer Id you entered does not exist");
                        return;
                    }

                    if (!_tripRepository.TransportationExists(transportationId))
                    {
                        Console.WriteLine("The transportation Id you entered does not exist");
                        return;
                    }

                    if (!_tripRepository.AccommodationExists(accommodationId))
                    {
                        Console.WriteLine("The accommodation Id you entered does not exist");
                        return; 
                    }

                    if (!_tripRepository.ServiceExists(serviceId))
                    {
                        Console.WriteLine("The service Id you entered does not exist");
                        return;
                    }

                    if (!_tripRepository.IsServiceAvailable(serviceId))
                    {
                        Console.WriteLine("The service you chose is not available at the moment");
                        return;
                    }

                    if (!_tripRepository.IsAccommodationAvailable(accommodationId))
                    {
                        Console.WriteLine("The accommodation you chose is not available at the moment");
                        return;
                    }

                    if (!_tripRepository.IsTransportationAvailable(transportationId))
                    {
                        Console.WriteLine("The transportation you chose is not available at the moment");
                        return;
                    }

                        _tripRepository.BookTransportation(customerId, transportationId);
                        _tripRepository.BookAccommodation(customerId, accommodationId, daysOfVisit);
                        _tripRepository.BookService(customerId, serviceId);

                Console.WriteLine("Your trip was booked sucessfully");


            }
            catch(Exception ex)
            {
                throw new Exception("An error occured while trying to book a trip");
            }



        }

        public void ShowCustomerInvoices(long customerId)
        {
            if (_customerRepository.CustomerExists(customerId))
            {
                DateTime dateRangeStart = DateTime.MinValue;
                DateTime dateRangeEnd = DateTime.Now;

                Console.WriteLine("\n\nYour paid Invoices: \n");
                List<Invoice> paidInvoices = _analyticsRepository.PaidInvoicesByCustomer(customerId, dateRangeStart, dateRangeEnd).ToList();
                if (paidInvoices.Any())
                {
                    foreach (Invoice invoice in paidInvoices)
                    {
                        Console.WriteLine($"Invoice Id: {invoice.InvoiceId}");
                        Console.WriteLine($"Date of Issue: {invoice.IssuedDate.Date}");
                        Console.WriteLine($"Date of Payment: {invoice.PaymentDate?.Date}");
                        Console.WriteLine($"Payment Amount: {invoice.TotalAmount}");
                    }
                }
                else
                {
                    Console.WriteLine("\n\nThere are no paid invoices for this Id");
                }

                Console.WriteLine("\n\nYour unpaid Invoices: \n");
                List<Invoice> unPaidInvoices = _analyticsRepository.UnPaidInvoicesByCustomer(customerId, dateRangeStart, dateRangeEnd).ToList();

                if (unPaidInvoices.Any())
                {
                    foreach (Invoice invoice in paidInvoices)
                    {
                        Console.WriteLine($"Invoice Id: {invoice.InvoiceId}");
                        Console.WriteLine($"Date of Issue: {invoice.IssuedDate.Date}");
                        Console.WriteLine($"Payment Amount: {invoice.TotalAmount}");
                    }
                }
                else
                {
                    Console.WriteLine("\n\nThere are no unpaid invoices for this Id");
                }
            }
            else
            {
                Console.WriteLine("The customer id you entered does not exist");
            }
        }
        public void PayInvoice(long customerId, decimal amount, long invoiceId)
        {
            if (!_analyticsRepository.InvoiceExists(invoiceId))
            {
                Console.WriteLine($"Invoice with Id: {invoiceId} does not exist");
                return;
            }

            if (!_customerRepository.CustomerExists(customerId))
            {
                Console.WriteLine($"Customer with Id: {customerId} does not exist.");
                return;
            }
            
                PayInvoice(customerId, amount, invoiceId);
            
        }

        public void TopChoicesByCustomers()
        {
            Console.WriteLine("\n\nThe following are the top 10 Choices by our Customers so far: ");

            Console.WriteLine("-----Top 10 Destinations-----");
            List<Destination> top10Destinations = _analyticsRepository.Top10Destinations().ToList();
            if( !top10Destinations.Any() )
            {
                Console.WriteLine("Top Destinations List is not available at the moment. Please try again later.");
            }
            else
            {
                for (int i = 0; i < top10Destinations.Count; i++)
                {
                    Destination destination = top10Destinations[i];
                    Console.WriteLine($"\n\n\n{i + 1}. Country: {destination.Country} || City: {destination.City} || {destination.DestinationId}");
                }
            }


            Console.WriteLine("-----Top 10 Hotels-----");

            List<Accommodation> top10Hotels = _analyticsRepository.Top10MostVisitedAccommodations().ToList();
            if (!top10Hotels.Any())
            {
                Console.WriteLine("Top Hotels List is not available at the moment. Please try again later.");
            }
            else
            {
                for (int i = 0; i < top10Hotels.Count; i++)
                {
                    Accommodation accommodation = top10Hotels[i];
                    Console.WriteLine($"\n\n\n{i + 1}. Hotel: {accommodation.HotelName} || Star Rating: {accommodation.StarRating} ||" +
                        $" Price Per Person: {accommodation.PricePerPersonPerDay} || Accommodation Id: {accommodation.AccommodationId}");

                }
            }



            Console.WriteLine("-----Top 10 Additional Services-----");

            List<Service> top10Services = _analyticsRepository.Top10Services().ToList();
            if (!top10Services.Any())
            {
                Console.WriteLine("Top Services List is not available at the moment. Please try again later.");
            }
            else
            {
                for (int i = 0; i < top10Services.Count; i++)
                {
                    Service service = top10Services[i];
                    Console.WriteLine($"\n\n\n{i + 1}. Service Description: {service.ServiceName} || Price: {service.Price} ||" +
                        $" Service Id: {service.ServiceId}");

                }
            }
        }
    }
}
