using System.Collections.Generic;
using Models.Classes;
using Models.Enums;

namespace PVPService.Services
{
    public class AccountsRepository
    {
        private List<AccountModel> _accounts = new List<AccountModel>()
        {
            new AccountModel("Originals", "Test123"),
            new AccountModel("No originals", null)
        };

        private Database _database = new Database();

        public bool IsAccountRegistered(string username)
        {
            if (username == null)
                return false;

            return _database.GetAccounts().Find((account) => account.Username == username) != null;
        }

        public bool IsEmailRegistered(string email)
        {
            if (email == null)
                return false;

            return _database.GetAccounts().Find((account) => account.Email == email) != null;
        }

        public SignInResponseCode RegisterNewAccount(AccountModel account)
        {
            if (account == null)
                return SignInResponseCode.UnknowError;

            else if (IsAccountRegistered(account.Username))
                return SignInResponseCode.UsernameAlreadyRegistered;

            else if (IsEmailRegistered(account.Email))
                return SignInResponseCode.EmailAlreadyUsed;
            
            var added =_database.AddAccount(account);
            added = added && _database.AddTrainer(new TrainerModel
            {
                Username = account.Username,
                Level = 40
            });

            if (added)
                return SignInResponseCode.SignInSuccessful;
            else
                return SignInResponseCode.UnknowError;
        }

        public LogInResponseCode ValidateCredentials(AccountModel account)
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
