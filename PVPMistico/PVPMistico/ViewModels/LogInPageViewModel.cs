using System;
using Prism.Navigation;
using PVPMistico.Views;
using Xamarin.Forms;

namespace PVPMistico.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private string _password;
        private string _username;
        private bool _hidePassword = true;
        private string _passwordVisibilityIcon = "PasswordHidden.png";

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

        public Command LogInCommand { get; private set; }
        public Command SignInCommand { get; private set; }
        public Command PasswordVisibilityToggleCommand { get; private set; }

        public LogInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Inicia sessión";
            LogInCommand = new Command(OnLogInButtonPressed);
            SignInCommand = new Command(OnSignInButtonPressed);
            PasswordVisibilityToggleCommand = new Command(OnPasswordVisibilityToggle);
        }

        private void OnPasswordVisibilityToggle()
        {
            switch (HidePassword)
            {
                case true:
                    HidePassword = false;
                    PasswordVisibilityIcon = "PasswordVisible.png";
                    break;
                case false:
                    HidePassword = true;
                    PasswordVisibilityIcon = "PasswordHidden.png";
                    break;
            }
        }

        private void OnSignInButtonPressed()
        {
            NavigationService.NavigateAsync(nameof(SignInPage));
        }

        private void OnLogInButtonPressed()
        {
            NavigationService.NavigateAsync(nameof(MainPage));
        }
    }
}
