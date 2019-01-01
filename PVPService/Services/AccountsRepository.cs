using System.Collections.Generic;
using Models.Classes;
using Models.Enums;

namespace PVPService.Services
{
    public class AccountsRepository
    {
        private List<AccountModel> _accounts;

        public bool IsAccountRegistered(string username)
        {
            if (username == null)
                return false;

            return _accounts.Find((account) => account.Username == username) != null;
        }

        public bool RegisterNewAccount(AccountModel account)
        {
            if (IsAccountRegistered(account.Username) || account == null)
                return false;

            _accounts.Add(account);
            return true;
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
