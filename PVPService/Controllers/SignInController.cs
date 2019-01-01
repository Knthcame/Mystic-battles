﻿using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly AccountsRepository _accounts;
        [HttpPost]
        public HttpResponseMessage Post([FromBody] string json)
        {
            var response = new HttpResponseMessage();
            var account = JsonConvert.DeserializeObject<AccountModel>(json);
            var responseCode = _accounts.ValidateCredentials(account);

            switch (responseCode)
            {
                case LogInResponseCode.LogInSuccessful:
                    response.StatusCode = HttpStatusCode.OK;
                    break;

                default:
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    break;
            }
            response.ReasonPhrase = responseCode.ToString();
            return response;
        }
    }
}