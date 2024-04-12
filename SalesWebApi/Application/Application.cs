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
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ShowCustomerOrders(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ShowProducts()
        {
            throw new NotImplementedException();
        }
    }
}
