using System.Net;
using Models.ApiResponses.Interfaces;

namespace Models.ApiResponses
{
    public class CheckUsernameResponse : IApiResponse
    {
        public bool IsUsernameRegistered { get; set; }
    }
}
