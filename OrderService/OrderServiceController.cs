using Microsoft.AspNetCore.Mvc;

namespace OrderService;

public class OrderServiceController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateOrder()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            return Ok();
        }
    }
}