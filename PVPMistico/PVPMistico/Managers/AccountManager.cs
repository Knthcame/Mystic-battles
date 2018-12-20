using System;
using System.Threading.Tasks;
using PVPMistico.Constants;
using PVPMistico.Managers.Interfaces;
using Xamarin.Essentials;

namespace PVPMistico.Managers
{
    public class AccountManager : IAccountManager
    {
        IHttpManager HttpManager;
        public AccountManager(IHttpManager httpManager)
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

        public bool SignIn(string name, string email, string username, string password, out string signInResponse)
        {
            if(username == "Originals")
            {
                signInResponse = SignInResponses.UserAlreadyRegistered;
                return false;
            }
            else
            {
                SecureStorage.SetAsync(SecureStorageTokens.Username, username);
                SecureStorage.SetAsync(SecureStorageTokens.Name, name);
                SecureStorage.SetAsync(SecureStorageTokens.Email, email);

                signInResponse = SignInResponses.SignInSuccessfull;
                return true;
            }
        }

        public void LogOut()
        {
            SecureStorage.Remove(SecureStorageTokens.Username);
            SecureStorage.Remove(SecureStorageTokens.Email);
            SecureStorage.Remove(SecureStorageTokens.Name);
        }
    }
}
