using Models.ApiResponses.Interfaces;
using Models.Enums;

namespace Models.ApiResponses
{
    public class CreateLeaderboardResponse : IApiResponse
    {
        public CreateLeaderboardResponseCode ResponseCode { get; set; }
    }
}
