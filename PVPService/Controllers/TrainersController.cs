using Microsoft.AspNetCore.Mvc;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult GetRegisteredTrainers() 
            => Ok(TrainersRepository.GetTrainers());

        [HttpGet("{username}")]
        public IActionResult GetTrainer(string username) 
            => Ok(TrainersRepository.GetTrainer(username));
        
    }
}
