using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
using PVPMistico.Views;

namespace PVPMistico.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        #region Fields
        private string _passwordVisibilityIcon = AppImages.ViewPassword;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _username;
        private ValidatableObject<string> _email;
        private bool _hidePassword = true;
        private bool _isEmailValid;
        private bool _isNameValid;
        private bool _isUsernameValid;
        private bool _isPasswordValid;
        private bool _areCredentialsValid;
        #endregion

        #region Properties
        public ValidatableObject<string> Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public ValidatableObject<string> Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public ValidatableObject<string> Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string PasswordVisibilityIcon
        {
            get => _passwordVisibilityIcon;
            set => SetProperty(ref _passwordVisibilityIcon, value);
        }

        public bool HidePassword
        {
            get => _hidePassword;
            set => SetProperty(ref _hidePassword, value);
        }

        public bool AreCredentialsValid
        {
            get => _areCredentialsValid;
            set => SetProperty(ref _areCredentialsValid, value);
        }

        public IAccountManager AccountManager { get; private set; }
        public ICommand PasswordVisibilityToggleCommand { get; private set; }
        public ICommand SignInCommand { get; private set; }
        public ICommand EmailUnfocusedCommand { get; private set; }
        public ICommand NameUnfocusedCommand { get; private set; }
        public ICommand UsernameUnfocusedCommand { get; private set; }
        public ICommand PasswordUnfocusedCommand { get; private set; }
        #endregion

        public SignInPageViewModel(INavigationService navigationService, IAccountManager accountManager) : base(navigationService) 
        {
            Title = "Registro de cuenta";
            AccountManager = accountManager;

            PasswordVisibilityToggleCommand = new DelegateCommand(OnPasswordVisibilityToggle);
            SignInCommand = new DelegateCommand(OnSignInButtonClicked);
            EmailUnfocusedCommand = new DelegateCommand(OnEmailUnfocused);
            NameUnfocusedCommand = new DelegateCommand(OnNameUnfocused);
            UsernameUnfocusedCommand = new DelegateCommand(OnUsernameUnfocused);
            PasswordUnfocusedCommand = new DelegateCommand(OnPaswordUnfocused);

            InitializeValidatableObjects();
            AddValidations();
        }

        private void InitializeValidatableObjects()
        {
            _email = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();
            _username = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
        }

        private void AddValidations()
        {
            _email.Validations.Add(new IsEmailRule<string>());
            _name.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = "El nombre no puede estar vacio" });
            _username.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = "El usuario no puede estar vacio" });
            _username.Validations.Add(new IsUsernameAvailableRule(AccountManager));
            _password.Validations.Add(new IsPasswordFormatCorrectRule());
        }

        private void OnSignInButtonClicked()
        {
            if (!ValidateEmail() || !ValidateName())
                return;

            if (AccountManager.SignIn(Name.Value, Email.Value, Username.Value, Password.Value, out string signInResponse))
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
                    PasswordVisibilityIcon = AppImages.HidePassword;
                    break;

                case false:
                    HidePassword = true;
                    PasswordVisibilityIcon = AppImages.ViewPassword;
                    break;
            }
        }

        private void OnEmailUnfocused()
        {
            if (Email != null && Email.Value != null)
                Email.Value.Trim();

            _isEmailValid = ValidateEmail();
            CheckCredentials();
        }

        private void OnNameUnfocused()
        {
            if(Name != null && Name.Value != null)
                Name.Value.Trim();

            _isNameValid = ValidateName();
            CheckCredentials();
        }

        private void OnUsernameUnfocused()
        {
            if (Username != null && Username.Value != null)
                Username.Value.Trim();

            _isUsernameValid = ValidateUsername();
            CheckCredentials();
        }

        private void OnPaswordUnfocused()
        {
            if (Password != null && Password.Value != null)
                Password.Value.Trim();

            _isPasswordValid = ValidatePassword();
            CheckCredentials();
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidateName()
        {
            return _name.Validate();
        }

        private bool ValidateUsername()
        {
            return _username.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void CheckCredentials()
        {
            AreCredentialsValid = _isEmailValid && _isNameValid && _isUsernameValid && _isPasswordValid;
        }
    }
}
