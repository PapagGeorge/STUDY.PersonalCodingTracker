using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CreateOrderRequest
    {
        public Order Order { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
