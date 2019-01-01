using System.Net;
using Models.ApiResponses.Interfaces;

namespace Models.ApiResponses
{
    public class OkResponse : IApiResponse
    {
        public bool Ok { get; set; }
    }
}
