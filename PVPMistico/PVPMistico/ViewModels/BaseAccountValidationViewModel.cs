using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
using System.Windows.Input;

namespace PVPMistico.ViewModels
{
    public class BaseAccountValidationViewModel : BaseViewModel
    {
        #region Fields
        private string _passwordVisibilityIcon = AppImages.ViewPassword;
        protected ValidatableObject<string> _username;
        protected ValidatableObject<string> _password;
        protected bool _hidePassword = true;
        protected bool _isUsernameValid;
        protected bool _isPasswordValid;
        protected bool _areCredentialsValid = false;
        protected readonly IAccountManager _accountManager;
        protected readonly IDialogManager _dialogManager;
        #endregion

        #region Properties
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

        public ICommand UsernameUnfocusedCommand { get; protected set; }
        public ICommand PasswordUnfocusedCommand { get; protected set; }
        public ICommand PasswordVisibilityToggleCommand { get; protected set; }
        #endregion
        public BaseAccountValidationViewModel(INavigationService navigationService, IAccountManager accountManager, IDialogManager dialogManager, ICustomLogger logger) : base(navigationService, logger)
        {
            _accountManager = accountManager;
            _dialogManager = dialogManager;

            PasswordVisibilityToggleCommand = new DelegateCommand(OnPasswordVisibilityToggle);
            UsernameUnfocusedCommand = new DelegateCommand(OnUsernameUnfocused);
            PasswordUnfocusedCommand = new DelegateCommand(OnPaswordUnfocused);

            InitializeValidatableObjects();
            AddValidations();
        }

        protected virtual void OnPasswordVisibilityToggle()
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

        protected virtual void OnUsernameUnfocused()
        {
            if (string.IsNullOrEmpty(Username.Value))
                return;

            if (Username != null && Username.Value != null)
                Username.Value = Username.Value.Trim();

            _isUsernameValid = ValidateUsername();
            CheckCredentials();
        }

        protected virtual void OnPaswordUnfocused()
        {
            if (string.IsNullOrEmpty(Password.Value))
                return;

            if (Password != null && Password.Value != null)
                Password.Value = Password.Value.Trim();

            _isPasswordValid = ValidatePassword();
            CheckCredentials();
        }

        protected virtual void AddValidations()
        {
            Username.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = AppResources.EmptyUsernameError });
            Password.Validations.Add(new IsPasswordFormatCorrectRule());
        }

        protected virtual void InitializeValidatableObjects()
        {
            Username = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
        }

        protected virtual bool ValidateUsername()
        {
            return Username.Validate();
        }

        protected virtual bool ValidatePassword()
        {
            return Password.Validate();
        }

        protected virtual void CheckCredentials()
        {
            AreCredentialsValid = _isUsernameValid && _isPasswordValid;
        }
    }
}
