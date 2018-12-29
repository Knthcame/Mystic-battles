using System.Threading.Tasks;
using Models.Classes;
using PVPMistico.Constants;
using PVPMistico.Managers.Interfaces;
using Xamarin.Essentials;

namespace PVPMistico.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IHttpManager _httpManager;
        public AccountManager(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<string> LogInAsync(string username, string password)
        {
            if (!username.Equals("Originals"))
            {
                return LogInResponses.UsernameNotFound;
            }
            else if (!password.Equals("Test123"))
            {
                return LogInResponses.PasswordIncorrect;
            }
            else
            {
                await SecureStorage.SetAsync(SecureStorageTokens.Username, username);
                return LogInResponses.LogInSuccesful;
            }
        }

        public bool CheckUsernameRegistered(string username)
        {
            return username == "Originals";
        }

        public async Task<string> SignInAsync(string name, string email, string username, string password)
        {
            if(username == "Originals")
            {
                return SignInResponses.UserAlreadyRegistered;
            }
            else
            {
                await SecureStorage.SetAsync(SecureStorageTokens.Username, username);
                await SecureStorage.SetAsync(SecureStorageTokens.Name, name);
                await SecureStorage.SetAsync(SecureStorageTokens.Email, email);
                await SecureStorage.SetAsync(SecureStorageTokens.Id, "1");

                return SignInResponses.SignInSuccessful;
            }
        }

        public void LogOut()
        {
            SecureStorage.Remove(SecureStorageTokens.Username);
            SecureStorage.Remove(SecureStorageTokens.Email);
            SecureStorage.Remove(SecureStorageTokens.Name);
            SecureStorage.Remove(SecureStorageTokens.Id);
        }

        /* Creates new ParticipantModel if user exists*/
        public ParticipantModel CreateParticipant(string username)
        {
            if (username == "Originals")
                return new ParticipantModel()
                {
                    IsAdmin = true,
                    Level = 40,
                    Losses = 0,
                    Wins = 0,
                    Matches = null,
                    Points = 0,
                    Position = 1,
                    Username = "Originals"
                };
            else
                return null;
        }

        public TrainerModel GetTrainer(string username)
        {
            if (username == "Originals")
                return new TrainerModel()
                {
                    Username = "Originals",
                    Level = 40
                };
            else
                return null;
        }
    }
}
