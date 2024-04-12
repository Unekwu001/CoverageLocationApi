using Microsoft.AspNetCore.Mvc;

namespace ipNXSalesPortalApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync()
        {
            
            return Ok();
        }
    }
}
