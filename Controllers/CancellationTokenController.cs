using CFA_EFC_WEPAPI.Data;
using CFA_EFC_WEPAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CFA_EFC_WEPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancellationTokenController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CancellationTokenController(AppDbContext context)
        {
            _context = context;
        }

        // ============================
        // GET: api/products
        // ============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll(
            CancellationToken cancellationToken)
        {
            try
            {
                var products = await _context.Product
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return Ok(products);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request was cancelled by the client");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        //ASP.NET Core automatically cancels the request when:
        //We have CancellationToken parameter which is available by default in controller actions
        //Purpose of CancellationToken is to propagate notification that operations should be canceled
        //Browser tab is closed
        //Client disconnects
        //Network drops
        //HTTP client cancels the request
        [HttpGet("slow")]
        public async Task<IActionResult> Slow(CancellationToken token)
        {
            await Task.Delay(30000, token); // 30 sec
            return Ok("Completed");
        }

    }
}
