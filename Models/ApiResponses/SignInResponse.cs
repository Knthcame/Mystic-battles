using Models.ApiResponses.Interfaces;
using Models.Enums;

namespace Models.ApiResponses
{
    public class SignInResponse : IApiResponse
    {
        public SignInResponseCode ResponseCode { get; set; }
    }
}
