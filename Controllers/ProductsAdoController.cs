using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFA_EFC_WEPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAdoController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductsAdoController(ProductRepository repository)
        {
            _repository = repository;
        }

        // ===============================
        // GET: api/productsapi
        // ===============================
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _repository.GetAllProducts();
            return Ok(products);
        }

        // ===============================
        // GET: api/productsapi/5
        // ===============================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }
    }

}

