using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;

namespace SalesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
            
        }



        [HttpPost]
        [Route("createOrder")]
        public ActionResult CreateOrder([FromBody] CreateOrderRequest orderRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _orderService.CreateOrder(orderRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the order. {ex.Message}");
            }
        }

        
       
    }


    
}


