using PVPMistico.Managers.Interfaces;
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
        public string ValidationMessage { get; set; } = "Este usuario ya existe";

        public bool Check(string value)
        {
            return AccountManager.CheckUsernameAvailable(value);
        }
    }
}
