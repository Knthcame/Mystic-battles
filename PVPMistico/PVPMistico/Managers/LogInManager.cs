using System;
using PVPMistico.Constants;
using PVPMistico.Managers.Interfaces;
using Xamarin.Essentials;

namespace PVPMistico.Managers
{
    public class LogInManager : ILogInManager
    {
        IHttpManager HttpManager;
        public LogInManager(IHttpManager httpManager)
        {
            HttpManager = httpManager;
        }

        public bool LogIn(string username, string password, out string logInResponse)
        {
            if (!username.Equals("Originals"))
            {
                logInResponse = LogInResponses.UsernameNotFound;
                return false;
            }
            else if (!password.Equals("test"))
            {
                logInResponse = LogInResponses.PasswordIncorrect;
                return false;
            }
            else
            {
                logInResponse = LogInResponses.LogInSuccesfull;
                SecureStorage.SetAsync(SecureStorageTokens.Username, username);
                return true;
            }
        }
    }
}
