using Microsoft.AspNetCore.Mvc;
using Models.ApiResponses;
using Models.Classes;
using PVPService.Services;

namespace PVPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardsController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult GetLeaderboards()
        {
            var response = new LeaderboardListResponse
            {
                Leaderboards = LeaderboardsRepository.GetLeaderboards()
            };
            return Ok(response);
        }

        [HttpGet("username/{username}")]
        public IActionResult GetUserLeaderBoards(string username)
        {
            var response = new LeaderboardListResponse
            {
                Leaderboards = LeaderboardsRepository.GetUserLeaderBoards(username)
            };
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetLeaderboard(int id)
        {
            var response = new LeaderboardResponse
            {
                Leaderboard = LeaderboardsRepository.GetLeaderboard(id)
            };
            return Ok(response);
        }

        [HttpPost("{id}")]
        public IActionResult InputMatch(int id, [FromBody] MatchModel match)
        {
            if (LeaderboardsRepository.InputMatch(id, match))
                return Ok();
            else
                return BadRequest();



        }
    }
}
