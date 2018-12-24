using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation.Rules.Interfaces;

namespace PVPMistico.Validation.Rules
{
    public class IsUsernameAvailableRule : IValidationRule<string>
    {
        private IAccountManager AccountManager;
        public IsUsernameAvailableRule(IAccountManager accountManager)
        {
            AccountManager = accountManager;
        }
        public string ValidationMessage { get; set; } = AppResources.UserAlreadyExistsError;

        public bool Check(string value)
        {
            return !AccountManager.CheckUsernameRegistered(value);
        }
    }
}
