using Models.Enums;
using System.Collections.Generic;
using PVPMistico.Constants;

namespace PVPMistico.Dictionaries
{
    public static class SignInResponsesDictionary
    {
        private static Dictionary<SignInResponseCode, string> _dictionary = new Dictionary<SignInResponseCode, string>()
        {
            { SignInResponseCode.SignInSuccessful, SignInResponses.SignInSuccessful },
            { SignInResponseCode.PasswordFormatInvalid, SignInResponses.PasswordFormatInvalid},
            { SignInResponseCode.UsernameAlreadyRegistered, SignInResponses.UserAlreadyRegistered },
            { SignInResponseCode.EmailAlreadyUsed, SignInResponses.EmailAlreadyUsed}
        };

        public static bool GetResponseString(SignInResponseCode code, out string response)
        {
            return _dictionary.TryGetValue(code, out response);
        }
    }
}
