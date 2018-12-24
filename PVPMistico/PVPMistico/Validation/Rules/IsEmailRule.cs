using PVPMistico.Resources;
using PVPMistico.Validation.Rules.Interfaces;
using System;

namespace PVPMistico.Validation.Rules
{
    public class IsEmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = AppResources.WrongEmailFormatError;

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

                var domainExtension = domain[domain.GetUpperBound(0)];
                if (domainExtension.Length < 2 || domainExtension.Length > 3)
                    return false;

                return mailAddress.Address == email;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
