using System;
namespace Models.Enums
{
    public enum SignInResponseCode
    {
        SignInSuccessful,
        UsernameAlreadyRegistered,
        EmailAlreadyUsed,
        PasswordFormatInvalid
    }
}
