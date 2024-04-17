using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiActionMethodsPractice.Models;
using WebApiActionMethodsPractice.Interfaces;

namespace WebApiActionMethodsPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(IProductRepository products)
        {
            _products = products;
        }

        private readonly IProductRepository _products;

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _products.GetAllProducts().ToList();

                if (products == null)
                {
                    return NotFound();
                }
                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _products.GetAllProducts().FirstOrDefault(prod => prod.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            

        }
    }
}
