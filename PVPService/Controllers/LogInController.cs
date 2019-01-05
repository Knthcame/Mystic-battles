using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{username}")]
        public IActionResult CheckUsernameRegistered(string username)
        {
            try
            {
                var response = _accounts.IsAccountRegistered(username);

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
                var response = _accounts.ValidateCredentials(account);
                switch (response)
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
