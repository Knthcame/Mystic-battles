using Models.ApiResponses.Interfaces;
using Models.Classes;
using System.Collections.Generic;

namespace Models.ApiResponses
{
    public class LeaderboardListResponse : IApiResponse
    {
        public List<LeaderboardModel> Leaderboards { get; set; }
    }
}
