using Microsoft.AspNetCore.Mvc;
using Models.ApiResponses;
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
            var response = new LeaderboardListResponse
            {
                Leaderboards = _leaderboards.GetLeaderboards()
            };
            return Ok(response);
        }

        [HttpGet("username/{username}")]
        public IActionResult GetUserLeaderBoards(string username)
        {
            var response = new LeaderboardListResponse
            {
                Leaderboards = _leaderboards.GetUserLeaderBoards(username)
            };
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetLeaderboard(int id)
        {
            var response = new LeaderboardResponse
            {
                Leaderboard = _leaderboards.GetLeaderboard(id)
            };
            return Ok(response);
        }

        [HttpPut("match/{id}")]
        public IActionResult InputMatch(int id, [FromBody] MatchModel match)
        {
            var response = new OkResponse()
            {
                Ok = _leaderboards.InputMatch(id, match)
            };
            if (response.Ok)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("trainer/{id}")]
        public IActionResult AddTrainer(int id, [FromBody] ParticipantModel trainer)
        {
            var response = new AddTrainerResponse
            {
                ResponseCode = _leaderboards.AddTrainer(id, trainer)
            };

            switch (response.ResponseCode)
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
            var response = new CreateLeaderboardResponse
            {
                ResponseCode = _leaderboards.AddLeaderboard(leaderboard)
            };

            switch (response.ResponseCode)
            {
                case CreateLeaderboardResponseCode.CreatedSuccessfully:
                    return Ok(response);

                default:
                    return BadRequest(response);
            }
        }
    }
}
