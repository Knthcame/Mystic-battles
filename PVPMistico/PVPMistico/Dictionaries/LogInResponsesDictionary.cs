using System.Collections.Generic;
using Models.Enums;
using PVPMistico.Constants;

namespace PVPMistico.Dictionaries
{
    public static class LogInResponsesDictionary
    {
        private static Dictionary<LogInResponseCode, string> _dictionary = new Dictionary<LogInResponseCode, string>()
        {
            { LogInResponseCode.LogInSuccessful, LogInResponses.LogInSuccesfull },
            { LogInResponseCode.PasswordIncorrect, LogInResponses.PasswordIncorrect },
            { LogInResponseCode.UsernameNotRegistered, LogInResponses.UsernameNotFound }
        };

        public static bool GetResponseString(LogInResponseCode code, out string response)
        {
            return _dictionary.TryGetValue(code, out response);
        }
    }
}