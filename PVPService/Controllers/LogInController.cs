using Microsoft.AspNetCore.Mvc;
using Models.ApiResponses;
using Models.Classes;
using Models.Enums;
using Newtonsoft.Json;
using PVPService.Services;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private AccountsRepository _accounts = new AccountsRepository();
        // POST api/values
        [HttpPost]
        public IActionResult ValidateLogIn([FromBody] AccountModel account)
        {
            LogInResponse response = new LogInResponse();
            var responseCode = _accounts.ValidateCredentials(account);
            
            response.ResponseCode = responseCode;
            switch (responseCode)
            {
                case LogInResponseCode.LogInSuccessful:
                    response.StatusCode = HttpStatusCode.OK;
                    return Ok(response);
                default:
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    return BadRequest(response);
            }
        }
    }
}
