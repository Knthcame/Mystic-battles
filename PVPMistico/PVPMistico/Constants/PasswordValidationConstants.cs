namespace PVPMistico.Constants
{
    public static class PasswordValidationConstants
    {
        public const string MinimumCharacters = "6";

        public const string MaximumCharacters = "15";

        public static string PasswordFormatInvalid =
                        $"La contraseña debe contener entre {MinimumCharacters} y {MaximumCharacters} carácteres, " +
                        $"incluyendo una letra mayúscula, una minúscula y un número.";
    }
}
