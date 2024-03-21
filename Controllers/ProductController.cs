using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PatronusBazar.BL;
using PatronusBazar.Models;

namespace PatronusBazar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        readonly ContextDB db = new ContextDB();

        [HttpPost("/createproduct")]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Invalid product data");
                }

                db.CreateProduct(product);

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error creating product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("/products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                List<Product> products = db.GetAllProducts();

                if (products.Count == 0)
                {
                    var hardcodedProduct = new Product
                    {
                        Id = 1,
                        Title = "Default Product",
                        // Add other properties as needed
                    };

                    return Ok(new List<Product> { hardcodedProduct });
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error retrieving products: {ex.Message}");

                return StatusCode(500, "An error occurred while fetching products.");
            }
        }

        [HttpGet("/product/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            try
            {
                Product product = db.GetProductById(id);

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error retrieving product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("/updateproduct/{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                if (updatedProduct == null)
                {
                    return BadRequest("Invalid product data");
                }

                Product existingProduct = db.GetProductById(id);

                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }

                // Update properties of the existing product
                existingProduct.Title = updatedProduct.Title;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.DiscountPercentage = updatedProduct.DiscountPercentage;
                existingProduct.Rating = updatedProduct.Rating;
                existingProduct.Stock = updatedProduct.Stock;
                existingProduct.Brand = updatedProduct.Brand;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.Thumbnail = updatedProduct.Thumbnail;
                existingProduct.Image1 = updatedProduct.Image1; // New fields
                existingProduct.Image2 = updatedProduct.Image2; // New fields
                existingProduct.Image3 = updatedProduct.Image3; // New fields
                existingProduct.Image4 = updatedProduct.Image4; // New fields

                db.UpdateProduct(existingProduct);

                return Ok(existingProduct);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error updating product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("/deleteproduct/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                bool deletionSuccessful = db.DeleteProduct(id);

                if (deletionSuccessful)
                {
                    return Ok("Product deleted successfully");
                }
                else
                {
                    return NotFound("Product not found or deletion failed");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.Error.WriteLine($"Error deleting product: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
