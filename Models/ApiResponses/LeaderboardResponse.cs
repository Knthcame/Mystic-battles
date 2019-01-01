using Models.ApiResponses.Interfaces;
using Models.Classes;

namespace Models.ApiResponses
{
    public class LeaderboardResponse : IApiResponse
    {
        public LeaderboardModel Leaderboard { get; set; }
    }
}
