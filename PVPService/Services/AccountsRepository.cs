using System.Collections.Generic;
using Models.Classes;
using Models.Enums;

namespace PVPService.Services
{
    public static class AccountsRepository
    {
        private static List<AccountModel> _accounts = new List<AccountModel>()
        {
            new AccountModel("Originals", "Test123"),
            new AccountModel("No originals", null)
        };

        public static bool IsAccountRegistered(string username)
        {
            if (username == null)
                return false;

            return _accounts.Find((account) => account.Username == username) != null;
        }

        public static bool IsEmailRegistered(string email)
        {
            if (email == null)
                return false;

            return _accounts.Find((account) => account.Email == email) != null;
        }

        public static SignInResponseCode RegisterNewAccount(AccountModel account)
        {
            if (account == null)
                return SignInResponseCode.UnknowError;

            else if (IsAccountRegistered(account.Username))
                return SignInResponseCode.UsernameAlreadyRegistered;

            else if (IsEmailRegistered(account.Email))
                return SignInResponseCode.EmailAlreadyUsed;
            
            _accounts.Add(account);
            TrainersRepository.AddTrainer(new TrainerModel
            {
                Username = account.Username,
                Level = 40
            });
            return SignInResponseCode.SignInSuccessful;
        }

        public static LogInResponseCode ValidateCredentials(AccountModel account)
        {
            if (!IsAccountRegistered(account.Username))
                return LogInResponseCode.UsernameNotRegistered;

            var registeredAccount = _accounts.Find((user) => user.Username == account.Username);
            if (registeredAccount.Password != account.Password)
                return LogInResponseCode.PasswordIncorrect;
            else
                return LogInResponseCode.LogInSuccessful;

        }
    }
}
