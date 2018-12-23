using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Text;
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

        public IAccountManager AccountManager { get; protected set; }
        public ICommand UsernameUnfocusedCommand { get; protected set; }
        public ICommand PasswordUnfocusedCommand { get; protected set; }
        public ICommand PasswordVisibilityToggleCommand { get; protected set; }
        #endregion
        public BaseAccountValidationViewModel(INavigationService navigationService, IAccountManager accountManager) : base(navigationService)
        {
            AccountManager = accountManager;

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
            if (Username != null && Username.Value != null)
                Username.Value.Trim();

            _isUsernameValid = ValidateUsername();
            CheckCredentials();
        }

        protected virtual void OnPaswordUnfocused()
        {
            if (Password != null && Password.Value != null)
                Password.Value.Trim();

            _isPasswordValid = ValidatePassword();
            CheckCredentials();
        }

        protected virtual void AddValidations()
        {
            Username.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = "El usuario no puede estar vacio" });
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
