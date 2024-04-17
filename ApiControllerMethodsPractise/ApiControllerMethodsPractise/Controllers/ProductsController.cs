using ApiControllerMethodsPractise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiControllerMethodsPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _productList = new List<Product>
        {
            new Product {ProductId = 1, ProductTitle = "Coca-Cola", Price = 1.30m, Availability = 10, isAvailable = true},
            new Product {ProductId = 2, ProductTitle = "Pepsi", Price = 1.10m, Availability = 8, isAvailable = true},
            new Product {ProductId = 3, ProductTitle = "Gum", Price = 1.50m, Availability = 0, isAvailable = false},
            new Product {ProductId = 4, ProductTitle = "Neswspaper", Price = 2m, Availability = 4, isAvailable = true},
            new Product {ProductId = 5, ProductTitle = "Chocolate", Price = 4m, Availability = 7, isAvailable = true},
            new Product {ProductId = 6, ProductTitle = "PopCorn", Price = 4m, Availability = 0, isAvailable = false},
            new Product {ProductId = 7, ProductTitle = "Magazine", Price = 7m, Availability = 5, isAvailable = true}
        };
        public ProductsController(List<Product> productList)
        {
            _productList = productList;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productList.ToList();
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
            var product = _productList.FirstOrDefault(prod => prod.ProductId == id);

            if(product == null)
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
            var availableProducts = _productList.Where(prod => prod.isAvailable == true).ToList();

            if(availableProducts.Count == 0)
            {
                return NoContent();
            }

            if(availableProducts == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(availableProducts);
            }
        }

        [HttpGet("price-range")]
        public IActionResult GetProductsInPriceRange([FromQuery]decimal minPrice, [FromQuery]decimal maxPrice)
        {
            var productsInPriceRange = _productList.Where(prod => prod.Price >= 4 && prod.Price <= 8).ToList();

            if(productsInPriceRange.Count == 0)
            {
                return NoContent();
            }
            
            if(productsInPriceRange == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(productsInPriceRange);
            }
        }



        
    }
}
