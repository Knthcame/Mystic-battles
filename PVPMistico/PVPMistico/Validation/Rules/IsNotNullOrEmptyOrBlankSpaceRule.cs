using PVPMistico.Resources;
using PVPMistico.Validation.Rules.Interfaces;

namespace PVPMistico.Validation.Rules
{
    public class IsNotNullOrEmptyOrBlankSpaceRule<T> : IValidationRule<T> where T : class
    {
        public string ValidationMessage { get; set; } = AppResources.EmptyEntryError;

        public bool Check(T value)
        {
            string str = value as string;
            if (str == null)
                return false;

            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }
    }
}
