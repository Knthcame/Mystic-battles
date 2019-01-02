using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Models.Enums;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardsController : Controller
    {
        private LeaderboardsRepository _leaderboards = new LeaderboardsRepository();
        // GET: /<controller>/
        [HttpGet]
        public IActionResult GetLeaderboards()
        {
            var response = _leaderboards.GetLeaderboards();
            return Ok(response);
        }

        [HttpGet("username/{username}")]
        public IActionResult GetUserLeaderBoards(string username)
        {
            var response = _leaderboards.GetUserLeaderBoards(username);
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetLeaderboard(int id)
        {
            var response = _leaderboards.GetLeaderboard(id);
            return Ok(response);
        }

        [HttpPut("match/{id}")]
        public IActionResult InputMatch(int id, [FromBody] MatchModel match)
        {
            var ok = _leaderboards.InputMatch(id, match);
            if (ok)
                return Ok(ok);
            else
                return BadRequest(ok);
        }

        [HttpPut("trainer/{id}")]
        public IActionResult AddTrainer(int id, [FromBody] ParticipantModel trainer)
        {
            var response = _leaderboards.AddTrainer(id, trainer);

            switch (response)
            {
                case AddTrainerResponseCode.TrainerAddedSuccesfully:
                    return Ok(response);

                default:
                    return BadRequest(response);
            }
        }

        [HttpPut]
        public IActionResult CreateLeaderboard([FromBody] LeaderboardModel leaderboard)
        {
            var response = _leaderboards.AddLeaderboard(leaderboard);

            switch (response)
            {
                case CreateLeaderboardResponseCode.CreatedSuccessfully:
                    return Ok(response);

                default:
                    return BadRequest(response);
            }
        }
    }
}
