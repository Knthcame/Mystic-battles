using System;
namespace PVPMistico.Managers.Interfaces
{
    public interface ILogInManager
    {
        bool LogIn(string username, string password, out string logInResponse);
    }
}
