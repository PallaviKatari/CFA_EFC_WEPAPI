using CFA_EFC_WEPAPI.Data;
using CFA_EFC_WEPAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CFA_EFC_WEPAPI.v2.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        // POST: api/products
        // It ignores the route prefix and uses the absolute path instead
        // Below is an example of overriding the route prefix which is the absolute path
        [HttpPost("/api/products/create", Name = "CreateProductV2")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                throw new ArgumentException("Invalid product data.");

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product created successfully",
                data = product
            });
        }

        // READ ALL
        // GET: api/products
        [HttpPost("/api/products/getall", Name = "GetAllProductsV2")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(new
            {
                message = "Products retrieved successfully",
                data = products
            });
        }

        // READ BY ID
        // GET: api/products/5
        [HttpGet("{id:int:min(1)}", Name = "GetProductByIdV2")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            return Ok(new
            {
                message = "Product retrieved successfully",
                data = product
            });
        }

        // UPDATE
        // PUT: api/products/5
        [HttpPut("{id:int:min(1)}", Name = "UpdateProductV2")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                throw new ArgumentException("Product ID mismatch.");

            var product = await _context.Product.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Product updated successfully",
                data = product
            });
        }

        // DELETE
        // DELETE: api/products/5
        [HttpDelete("{id:int:min(1)}", Name = "DeleteProductV2")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product deleted successfully",
                data = product
            });
        }
    }
}
