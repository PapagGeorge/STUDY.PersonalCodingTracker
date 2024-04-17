using ApiControllerMethodsPractise.Interfaces;
using ApiControllerMethodsPractise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiControllerMethodsPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;



        public ProductsController(IProductRepository productRepository)
        {

            _productRepository = productRepository;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts().ToList();
            if (products == null)
            {
                return NotFound();
            }

            if (products.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {

            var product = _productRepository.GetProducts().FirstOrDefault(prod => prod.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(product);
            }

        }

        [HttpGet("available")]
        public IActionResult AvailableProducts()
        {

            var availableProducts = _productRepository.GetProducts().Where(prod => prod.isAvailable == true).ToList();

            if (availableProducts.Count == 0)
            {
                return NoContent();
            }

            if (availableProducts == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(availableProducts);
            }
        }

        [HttpGet("price-range")]
        public IActionResult GetProductsInPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {

            var productsInPriceRange = _productRepository.GetProducts().Where(prod => prod.Price >= minPrice && prod.Price <= maxPrice).ToList();

            if (productsInPriceRange.Count == 0)
            {
                return NoContent();
            }

            if (productsInPriceRange == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(productsInPriceRange);
            }
        }

        [HttpPost]
        [Route("crete-product")]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           return CreatedAtAction(nameof(GetProduct), new {id = product.ProductId}, product);
        }



        
    }
}
