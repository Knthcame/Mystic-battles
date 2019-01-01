using Microsoft.AspNetCore.Mvc;
using Models.ApiResponses;
using Models.Classes;
using Models.Enums;
using PVPService.Services;
using System;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private AccountsRepository _accounts = new AccountsRepository();

        //GET api/LogIn
        [HttpGet]
        public IActionResult CheckUsernameRegistered([FromRoute] string username)
        {
            try
            {
                var response = new CheckUsernameResponse
                {
                    IsUsernameRegistered = _accounts.IsAccountRegistered(username)
                };

                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/LogIn
        [HttpPost]
        public IActionResult ValidateLogIn([FromBody] AccountModel account)
        {
            try
            {
                LogInResponse response = new LogInResponse();
                var responseCode = _accounts.ValidateCredentials(account);

                response.ResponseCode = responseCode;
                switch (responseCode)
                {
                    case LogInResponseCode.LogInSuccessful:
                        return Ok(response);
                    default:
                        return BadRequest(response);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
