using Models.ApiResponses.Interfaces;
using Models.Enums;

namespace Models.ApiResponses
{
    public class AddTrainerResponse : IApiResponse
    {
        public AddTrainerResponseCode ResponseCode { get; set; }
    }
}
