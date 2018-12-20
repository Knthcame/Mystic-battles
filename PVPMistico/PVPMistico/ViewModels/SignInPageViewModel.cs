using System;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Views;

namespace PVPMistico.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        #region Fields
        private string _password;
        private string _passwordVisibilityIcon = "ViewPassword.png";
        private bool _hidePassword = true;
        private string _name;
        private string _username;
        private string _email;
        #endregion

        #region Properties
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool HidePassword
        {
            get => _hidePassword;
            set => SetProperty(ref _hidePassword, value);
        }

        public string PasswordVisibilityIcon
        {
            get => _passwordVisibilityIcon;
            set => SetProperty(ref _passwordVisibilityIcon, value);
        }

        public IAccountManager AccountManager { get; private set; }
        public ICommand PasswordVisibilityToggleCommand { get; private set; }
        public ICommand SignInCommand { get; private set; }
        #endregion

        public SignInPageViewModel(INavigationService navigationService, IAccountManager accountManager) : base(navigationService) 
        {
            Title = "Registro de cuenta";
            AccountManager = accountManager;

            PasswordVisibilityToggleCommand = new DelegateCommand(OnPasswordVisibilityToggle);
            SignInCommand = new DelegateCommand(OnSignInButtonClicked);
        }

        private void OnSignInButtonClicked()
        {
            if (AccountManager.SignIn(Name, Email, Username, Password, out string signInResponse))
                NavigationService.NavigateAsync("/NavigationPage/" + nameof(MainPage));

            var toastConfig = new ToastConfig(signInResponse);
            UserDialogs.Instance.Toast(toastConfig);
        }

        private void OnPasswordVisibilityToggle()
        {
            switch (HidePassword)
            {
                case true:
                    HidePassword = false;
                    PasswordVisibilityIcon = "HidePassword.png";
                    break;

                case false:
                    HidePassword = true;
                    PasswordVisibilityIcon = "ViewPassword.png";
                    break;
            }
        }
    }
}
