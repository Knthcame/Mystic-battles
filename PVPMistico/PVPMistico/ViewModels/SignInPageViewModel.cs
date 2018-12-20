using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
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
        private ValidatableObject<string> _email;
        private bool _isemailValid;
        #endregion

        #region Properties
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public ValidatableObject<string> Email
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

        public bool IsEmailValid
        {
            get => _isemailValid;
            set => SetProperty(ref _isemailValid, value);
        }

        public string PasswordVisibilityIcon
        {
            get => _passwordVisibilityIcon;
            set => SetProperty(ref _passwordVisibilityIcon, value);
        }

        public Color ErrorColor { get; set; }

        public IAccountManager AccountManager { get; private set; }
        public ICommand PasswordVisibilityToggleCommand { get; private set; }
        public ICommand SignInCommand { get; private set; }
        public ICommand EmailUnfocusedCommand { get; private set; }
        #endregion

        public SignInPageViewModel(INavigationService navigationService, IAccountManager accountManager) : base(navigationService) 
        {
            Title = "Registro de cuenta";
            AccountManager = accountManager;
            ErrorColor = Color.Red;

            PasswordVisibilityToggleCommand = new DelegateCommand(OnPasswordVisibilityToggle);
            SignInCommand = new DelegateCommand(OnSignInButtonClicked);
            EmailUnfocusedCommand = new DelegateCommand(OnEmailUnfocused);

            InitializeValidatableObjects();
            AddValidations();
        }

        private void InitializeValidatableObjects()
        {
            _email = new ValidatableObject<string>();
        }

        private void AddValidations()
        {
            var emailRule = new IsEmailRule<string>();
            try
            {
                _email.Validations.Add(emailRule);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void OnSignInButtonClicked()
        {
            if (AccountManager.SignIn(Name, Email.Value, Username, Password, out string signInResponse))
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

        private void OnEmailUnfocused()
        {
            IsEmailValid = Email.Validate();
        }
    }
}
