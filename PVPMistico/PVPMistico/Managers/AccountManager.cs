using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<LogInResponseCode> LogInAsync(AccountModel account)
        {
            var response = await _httpManager.PostAsync<LogInResponseCode>(ApiConstants.LogInURL, account);

            switch (response)
            {
                case LogInResponseCode.LogInSuccessful:
                    await SecureStorage.SetAsync(SecureStorageTokens.Username, account.Username);
                    break;
            }
            return response;
        }

        public async Task<bool> CheckUsernameRegisteredAsync(string username)
        {
            try
            {
                var response = await _httpManager.GetAsync<bool>(ApiConstants.LogInURL, parameter: username);

                return response;
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public async Task<SignInResponseCode> SignInAsync(SignInModels models)
        {
            var response = await _httpManager.PostAsync<SignInResponseCode>(ApiConstants.SignInURL, models);
            
            switch (response)
            {
                case SignInResponseCode.SignInSuccessful:
                    await SecureStorage.SetAsync(SecureStorageTokens.Username, models.Account.Username);
                    await SecureStorage.SetAsync(SecureStorageTokens.Name, models.Account.Name);
                    await SecureStorage.SetAsync(SecureStorageTokens.Email, models.Account.Email);
                    await SecureStorage.SetAsync(SecureStorageTokens.Password, models.Account.Password);

                    break;
            }

            return response;
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
            var trainer = await _httpManager.GetAsync<TrainerModel>(ApiConstants.TrainersURL, parameter: username);
            if (trainer == null)
                return null;

            return new ParticipantModel()
            {
                IsAdmin = isAdmin,
                Level = trainer.Level,
                Position = 1,
                Username = username
            };
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
