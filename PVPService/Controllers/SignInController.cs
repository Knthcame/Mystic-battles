using System;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Models.Enums;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private AccountsRepository _accounts = new AccountsRepository();

        private TrainersRepository _trainers = new TrainersRepository();

        [HttpPost]
        public IActionResult Post([FromBody] SignInModels models)
        {
            try
            {
                if (models == null || models.Account == null || models.Trainer == null)
                    return BadRequest(SignInResponseCode.UnknowError);

                var response = _accounts.RegisterNewAccount(models.Account);
                switch (response)
                {
                    case SignInResponseCode.SignInSuccessful:
                        _trainers.AddTrainer(models.Trainer);
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