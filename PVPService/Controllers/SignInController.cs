using Microsoft.AspNetCore.Mvc;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string json)
        {

        }
    }
}
