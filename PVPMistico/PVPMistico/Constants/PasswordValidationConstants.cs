using PVPMistico.Resources;

namespace PVPMistico.Constants
{
    public static class PasswordValidationConstants
    {
        public const string MinimumCharacters = "6";

        public const string MaximumCharacters = "15";

        public static string PasswordFormatInvalid => GetPasswordFormatErrorString();

        private static string GetPasswordFormatErrorString()
        {
            string message = AppResources.PasswordFormatInvalidPlaceholder;
            message = message.Replace("minimum", MinimumCharacters)
                             .Replace("maximum", MaximumCharacters);
            return message;
        }
    }
}
