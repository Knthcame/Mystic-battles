using PVPMistico.Resources;

namespace PVPMistico.Constants
{
    public static class SignInResponses
    {
        public static string UsernameAlreadyRegistered = AppResources.UserAlreadyRegisteredResponse;

        public static string SignInSuccessful = AppResources.SignInSuccessfulResponse;

        public static string EmailAlreadyUsed = AppResources.EmailAlreadyUsedResponse;

        public static string PasswordFormatInvalid = PasswordValidationConstants.PasswordFormatInvalid;

        public static string UnknownError = AppResources.Error;
    }
}
