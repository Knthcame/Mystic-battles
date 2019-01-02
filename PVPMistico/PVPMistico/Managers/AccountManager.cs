using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.ApiResponses;
using Models.Classes;
using Models.Enums;
using PVPMistico.Constants;
using PVPMistico.Dictionaries;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
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
                var response = await _httpManager.GetAsync<OkResponse>(ApiConstants.LogInURL, parameter: username);

                return response.Ok;
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public async Task<string> SignInAsync(AccountModel account)
        {
            var response = await _httpManager.PostAsync<SignInResponse>(ApiConstants.SignInURL, account);
            
            switch (response.ResponseCode)
            {
                case SignInResponseCode.SignInSuccessful:
                    await SecureStorage.SetAsync(SecureStorageTokens.Username, account.Username);
                    await SecureStorage.SetAsync(SecureStorageTokens.Name, account.Name);
                    await SecureStorage.SetAsync(SecureStorageTokens.Email, account.Email);
                    await SecureStorage.SetAsync(SecureStorageTokens.Password, account.Password);

                    break;
            }

            if (SignInResponsesDictionary.GetResponseString(response.ResponseCode, out string message))
                return message;
            else
                return AppResources.Error;
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

        public async Task<TrainerModel> GetTrainer(string username)
        {
            return await _httpManager.GetAsync<TrainerModel>(ApiConstants.TrainersURL, parameter: username);
        }

        public async Task<IList<TrainerModel>> GetRegisteredTrainers()
        {
            return await _httpManager.GetAsync<IList<TrainerModel>>(ApiConstants.TrainersURL);
        }
    }
}
