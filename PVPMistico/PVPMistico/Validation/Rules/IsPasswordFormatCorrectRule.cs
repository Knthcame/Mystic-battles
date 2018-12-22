using PVPMistico.Constants;
using PVPMistico.Validation.Rules.Interfaces;
using System.Text.RegularExpressions;

namespace PVPMistico.Validation.Rules
{
    public class IsPasswordFormatCorrectRule : IValidationRule<string>
    {
        private Regex _hasNumber = new Regex(@"[0-9]+");
        private Regex _hasUpperCaseCharacter = new Regex(@"[A-Z]+");
        private Regex _hasLowerCaseCharacter = new Regex(@"[a-z]+");
        private Regex _hasMiniMaxChars = new Regex(@".{8,15}");
        private Regex _hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        public string ValidationMessage { get; set; } = 
            $"La contraseña debe contener entre {PasswordValidationConstants.MinimumCharacters} y {PasswordValidationConstants.MaximumCharacters} carácteres, " +
            $"incluyendo una letra mayúscula, una minúscula y un número.";

        public bool Check(string password)
        {
            if (password == null)
                return false;

            return _hasNumber.IsMatch(password)
                && _hasMiniMaxChars.IsMatch(password)
                && _hasLowerCaseCharacter.IsMatch(password)
                && _hasUpperCaseCharacter.IsMatch(password);
        }
    }
}
