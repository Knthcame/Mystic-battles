using PVPMistico.Validation.Rules.Interfaces;
using System;

namespace PVPMistico.Validation.Rules
{
    public class IsEmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = "Porfavor inserte un formato de e-mail válido.";

        public bool Check(T value)
        {
            if (value == null)
                return false;

            string email = value as string;

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                var host = mailAddress.Host;
                string[] domain = host.Split('.');

                if (domain.Length <= 1)
                    return false;

                if (domain[domain.GetUpperBound(0)].Length < 2)
                    return false;

                return mailAddress.Address == email;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }
    }
}
