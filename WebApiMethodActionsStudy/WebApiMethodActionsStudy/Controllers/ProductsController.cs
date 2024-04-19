using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMethodActionsStudy.Interfaces;
using WebApiMethodActionsStudy.Models;

namespace WebApiMethodActionsStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _products;
        public ProductsController(IProductRepository products)
        {
            _products = products;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _products.GetProducts().ToList();

                if(products.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(products);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _products.GetProducts().FirstOrDefault(prod => prod.ProductId == id);

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

        [HttpGet("search")]
        public IActionResult SearchProducts(string query)
        {
            try
            {
                var products = _products.GetProducts().Where(prod => prod.ProductName.Contains(query)).ToList();

               
                if(products.Count() == 0)
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

        [HttpPost("new-product")]
        public IActionResult CreateNewProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _products.AddProduct(product);
                return CreatedAtAction(nameof(GetProduct), new {id = product.ProductId}, product);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            try
            {
                
                if (product == null)
                {
                    return BadRequest("Product does not exist");
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest("Product is not in valid state");
                }
                _products.UpdateProduct(product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProductPrice(int id, [FromBody] decimal price)
        {
            try
            {
                var productToUpdate = _products.GetProducts().FirstOrDefault(prod => prod.ProductId == id);

                if (productToUpdate == null)
                {
                    return BadRequest("Product does not exist");
                }

                productToUpdate.Price = price;


                _products.UpdateProduct(productToUpdate);

                return Ok(productToUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

    }
}
