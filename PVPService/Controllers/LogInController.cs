using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Models.Enums;
using Newtonsoft.Json;
using PVPService.Services;
using System.Net;
using System.Net.Http;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private AccountsRepository _accounts = new AccountsRepository();
        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody] string json)
        {
            HttpResponseMessage response = new HttpResponseMessage();
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
