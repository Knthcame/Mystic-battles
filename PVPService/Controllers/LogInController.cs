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

        //GET api/LogIn
        [HttpGet("{username}")]
        public IActionResult CheckUsernameRegistered(string username)
        {
            try
            {
                var response = new OkResponse
                {
                    Ok = AccountsRepository.IsAccountRegistered(username)
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
                var responseCode = AccountsRepository.ValidateCredentials(account);

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
