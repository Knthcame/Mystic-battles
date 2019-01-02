using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Models.Classes;
using Models.Crypto;
using Models.Enums;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Enums;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.ViewModels.BaseViewModels;
using PVPMistico.Views;
using Xamarin.Forms;

namespace PVPMistico.ViewModels
{
    public class LogInPageViewModel : BaseAccountValidationViewModel
    {
        #region Properties
        public DelegateCommand LogInCommand { get; private set; }
        public DelegateCommand SignInCommand { get; private set; }
        #endregion

        public LogInPageViewModel(INavigationService navigationService, IAccountManager accountManager, IDialogManager dialogManager, ICustomLogger logger) 
            : base(navigationService, accountManager, dialogManager, logger)
        {
            Title = AppResources.LogInPageTitle;

            LogInCommand = new DelegateCommand(async () => await OnLogInButtonPressed());
            SignInCommand = new DelegateCommand(async () => await OnSignInButtonPressed());
        }

        protected override void AddValidations()
        {
            base.AddValidations();
        }

        private async Task OnSignInButtonPressed()
        {
            try
            {
                await NavigationService.NavigateAsync(nameof(SignInPage));
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        private async Task OnLogInButtonPressed()
        {
            CheckCredentials();
            if (!AreCredentialsValid)
                return;
            var encryptedPassword = Password.Value.Encrypt("Originals rule");
            var logInResponse = await _accountManager.LogInAsync(new AccountModel(Username.Value, encryptedPassword));
            
            switch (logInResponse)
            {
                case LogInResponseCode.LogInSuccessful:
                    await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(MainPage));
                    break;

                case LogInResponseCode.UsernameNotRegistered:
                    Username.Errors = new List<string> { AppResources.UserNotRegisteredError };
                    Username.IsValid = false;
                    AreCredentialsValid = false;
                    break;

                case LogInResponseCode.PasswordIncorrect:
                    Username.Errors = new List<string> { AppResources.PasswordIncorrectResponse };
                    Username.IsValid = false;
                    AreCredentialsValid = false;
                    break;

                default:
                    _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
                    break;
            }
        }
    }
}
