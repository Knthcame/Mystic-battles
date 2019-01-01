using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
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
            
            var logInResponse = await _accountManager.LogInAsync(new AccountModel(Username.Value, Password.Value));

            if (logInResponse == LogInResponses.LogInSuccesful)
                await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(MainPage));
            else
            {
                _dialogManager.ShowToast(new ToastConfig(logInResponse));

                var errors = new List<string>
                {
                    logInResponse
                };

                if (logInResponse.Equals(LogInResponses.UsernameNotFound))
                {
                    Username.Errors = errors;
                    Username.IsValid = false;
                }
                else if (logInResponse.Equals(LogInResponses.PasswordIncorrect))
                {
                    Password.Errors = errors;
                    Password.IsValid = false;
                }
            }
        }
    }
}
