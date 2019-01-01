﻿using Models.ApiResponses.Interfaces;
using Models.Enums;
using System.Net;

namespace Models.ApiResponses
{
    public class LogInResponse : IApiResponse
    {
        public LogInResponseCode ResponseCode { get; set; }
    }
}
