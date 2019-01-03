using Microsoft.AspNetCore.Mvc;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : Controller
    {
        private MatchesRepository _matches = new MatchesRepository();

        [HttpGet]
        public IActionResult GetMatches()
        {
            var response = _matches.GetMatches();
            return Ok(response);
        }

        [HttpGet("league/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_matches.GetLeagueMatches(id));
        }

        [HttpGet("username /{username}")]
        public IActionResult Get(string username)
        {
            return Ok(_matches.GetTrainerMatches(username));
        }

        [HttpGet("league/{id}/username /{username}")]
        public IActionResult Get(int id, string username)
        {
            return Ok(_matches.GetTrainerLeagueMatches(username, id));
        }
    }
}
