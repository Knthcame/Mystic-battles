using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ApiResponses;
using Models.Classes;
using Models.Enums;
using Newtonsoft.Json;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] AccountModel account)
        {
            try
            {
                var response = new SignInResponse
                {
                    ResponseCode = AccountsRepository.RegisterNewAccount(account)
                };

                switch (response.ResponseCode)
                {
                    case SignInResponseCode.SignInSuccessful:
                        return Ok(response);

                    default:
                        return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}