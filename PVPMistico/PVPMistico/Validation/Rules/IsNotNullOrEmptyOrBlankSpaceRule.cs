using PVPMistico.Validation.Rules.Interfaces;

namespace PVPMistico.Validation.Rules
{
    public class IsNotNullOrEmptyOrBlankSpaceRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = "Este campo no puede estar vacío";

        public bool Check(T value)
        {
            string str = value as string;
            if (str == null)
                return false;

            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }
    }
}
