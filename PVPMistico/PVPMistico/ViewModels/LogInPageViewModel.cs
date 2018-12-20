using System.Drawing;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Views;

namespace PVPMistico.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        #region Fields
        private string _password;
        private string _username;
        private bool _hidePassword = true;
        private string _passwordVisibilityIcon = "ViewPassword.png";
        private bool _signInEnabled;
        private Color _usernameColor = Color.Black;
        private Color _passwordColor = Color.Black;
        #endregion

        #region Properties
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

        public bool SignInEnabled
        {
            get => _signInEnabled;
            set => SetProperty(ref _signInEnabled, value);
        }

        public Color UsernameColor 
        { 
            get => _usernameColor; 
            set => SetProperty(ref _usernameColor, value);
        }

        public Color PasswordColor
        {
            get => _passwordColor;
            set => SetProperty(ref _passwordColor, value);
        }

        public IAccountManager AccountManager { get; private set; }
        public DelegateCommand LogInCommand { get; private set; }
        public DelegateCommand SignInCommand { get; private set; }
        public DelegateCommand PasswordVisibilityToggleCommand { get; private set; }
        public DelegateCommand TextChangedCommand { get; private set; }
        #endregion

        public LogInPageViewModel(INavigationService navigationService, IAccountManager accountManager) : base(navigationService)
        {
            Title = "Inicia sesión";

            AccountManager = accountManager;

            LogInCommand = new DelegateCommand(async () => await OnLogInButtonPressed());
            SignInCommand = new DelegateCommand(async () => await OnSignInButtonPressed());
            PasswordVisibilityToggleCommand = new DelegateCommand(OnPasswordVisibilityToggle);
            TextChangedCommand = new DelegateCommand(OnTextChanged);
        }

        private void OnTextChanged()
        {
            SignInEnabled = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            UsernameColor = Color.Black;
            PasswordColor = Color.Black;
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

        private async Task OnSignInButtonPressed()
        {
            await NavigationService.NavigateAsync(nameof(SignInPage));
        }

        private async Task OnLogInButtonPressed()
        {
            if (AccountManager.LogIn(Username, Password, out string logInResponse))
                await NavigationService.NavigateAsync("/NavigationPage/" + nameof(MainPage));
            else
            {
                var toastConfig = new ToastConfig(logInResponse);
                toastConfig.SetPosition(ToastPosition.Bottom);
                UserDialogs.Instance.Toast(toastConfig);
                switch (logInResponse)
                {
                    case LogInResponses.UsernameNotFound:
                        UsernameColor = Color.Red;
                        break;

                    case LogInResponses.PasswordIncorrect:
                        PasswordColor = Color.Red;
                        break;
                }
            }
        }
    }
}
