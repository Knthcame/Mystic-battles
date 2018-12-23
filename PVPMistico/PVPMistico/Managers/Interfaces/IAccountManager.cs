using System;
namespace PVPMistico.Managers.Interfaces
{
    public interface IAccountManager
    {
        bool CheckUsernameRegistered(string username);

        bool LogIn(string username, string password, out string logInResponse);

        bool SignIn(string name, string email, string username, string password, out string signInResponse);

        void LogOut();
    }
}
