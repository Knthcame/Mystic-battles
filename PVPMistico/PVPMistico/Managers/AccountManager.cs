using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.ApiResponses;
using Models.Classes;
using Models.Enums;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using Xamarin.Essentials;

namespace PVPMistico.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IHttpManager _httpManager;
        private readonly ICustomLogger _logger;

        public AccountManager(IHttpManager httpManager, ICustomLogger logger)
        {
            _httpManager = httpManager;
            _logger = logger;
        }

        public async Task<string> LogInAsync(AccountModel account)
        {
            var response = await _httpManager.PostAsync<LogInResponse>(ApiConstants.LogInURL, account);

            switch (response.ResponseCode)
            {
                case LogInResponseCode.UsernameNotRegistered:
                    return LogInResponses.UsernameNotFound;

                case LogInResponseCode.PasswordIncorrect:
                    return LogInResponses.PasswordIncorrect;

                case LogInResponseCode.LogInSuccessful:
                    await SecureStorage.SetAsync(SecureStorageTokens.Username, account.Username);
                    return LogInResponses.LogInSuccesful;

                default:
                    return LogInResponses.GeneralError;
            }
        }

        public async Task<bool> CheckUsernameRegisteredAsync(string username)
        {
            try
            {

                var response = await _httpManager.GetAsync<CheckUsernameResponse>(ApiConstants.LogInURL + username);

                return response.IsUsernameRegistered;
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
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

                return SignInResponses.SignInSuccessful;
            }
        }

        public void LogOut()
        {
            SecureStorage.Remove(SecureStorageTokens.Username);
            SecureStorage.Remove(SecureStorageTokens.Email);
            SecureStorage.Remove(SecureStorageTokens.Name);
        }

        /* Creates new ParticipantModel if user exists*/
        public async Task<ParticipantModel> CreateParticipantAsync(string username, bool isAdmin)
        {
            if (await CheckUsernameRegisteredAsync(username))
                return new ParticipantModel()
                {
                    IsAdmin = isAdmin,
                    Level = 40,
                    Position = 1,
                    Username = username
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

        public IList<TrainerModel> GetRegisteredTrainers()
        {
            return new List<TrainerModel>()
            {
                new TrainerModel()
                {
                    Level = 40,
                    Username = "Originals"
                },
                new TrainerModel()
                {
                    Level = 39,
                    Username = "No originals"
                }
            };
        }
    }
}
