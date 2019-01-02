using Microsoft.AspNetCore.Mvc;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : Controller
    {
        private TrainersRepository _trainers = new TrainersRepository();
        // GET: /<controller>/
        [HttpGet]
        public IActionResult GetRegisteredTrainers() 
            => Ok(_trainers.GetTrainers());

        [HttpGet("{username}")]
        public IActionResult GetTrainer(string username) 
            => Ok(_trainers.GetTrainer(username));
        
    }
}
