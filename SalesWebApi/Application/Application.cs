using Application.Interfaces;
using Domain.Entities;


namespace Application
{
    public class Application : IApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderService _orderService;
        

        public Application(IProductRepository productRepository, ICustomerRepository customerRepository, IOrderService orderService)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderService = orderService;
        }
        public void CreateAnOrder(int customerId, int paymentMethodId, List<Product> productList)
        {
            try
            {
                List<Product> orderProductList = _productRepository.ChosenProductsExist(productList).ToList();
                List <Product> orderProductListChecked = _productRepository.CheckProductListAvailability(orderProductList).ToList();
                decimal amount = 0;

                foreach (Product product in  orderProductList)
                {
                    amount += product.Price;

                }

                var order = new Order();
                order.PaymentMethodId = paymentMethodId;
                order.Amount = amount;
                order.customerId = customerId;

                _orderService.CreateOrder(order, productList);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new order. {ex.Message}");
            }
        }


        public void CreateNewCustomer(Customer customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.MobilePhone))
                {
                    throw new Exception("Customer's Name and Mobile Phone must be provided");
                }
                else
                {
                    _customerRepository.CreateNewCustomer(customer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new Customer. {ex.Message}");
            }
        }

        public IEnumerable<object> ShowCustomerOrders(int customerId)
        {
            if (_customerRepository.CustomerExists(customerId))
            {
                var ordersByCustomer =  _customerRepository.OrdersByCustomer(customerId).ToList();
                return ordersByCustomer;
            }
            else
            {
                throw new KeyNotFoundException($"Customer with Id: {customerId} was not found");
            }
        }

       
    }
}
