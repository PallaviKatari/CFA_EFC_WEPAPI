using Microsoft.AspNetCore.Mvc;

namespace CFA_EFC_WEPAPI.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("error")]
        public IActionResult ThrowError()
        {
            throw new Exception("Something went wrong!");
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundError()
        {
            throw new KeyNotFoundException();
        }
    }
}
