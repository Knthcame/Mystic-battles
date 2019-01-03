using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Models.Classes;
using Models.Crypto;
using Models.Enums;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Enums;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
using PVPMistico.ViewModels.BaseViewModels;
using PVPMistico.Views;
using Xamarin.Forms;

namespace PVPMistico.ViewModels
{
    public class SignInPageViewModel : BaseAccountValidationViewModel
    {
        #region Fields
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _confirmPassword;
        private bool _isEmailValid;
        private bool _isNameValid;
        private bool _isLevelValid;
        private bool _isConfirmPasswordValid;
        private bool _hideConfirmPassword = true;
        private string _confirmPasswordVisibilityIcon = AppImages.ViewPassword;
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

        public ValidatableObject<string> ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public bool HideConfirmPassword
        {
            get => _hideConfirmPassword;
            set => SetProperty(ref _hideConfirmPassword, value);
        }

        public string ConfirmPasswordVisibilityIcon
        {
            get => _confirmPasswordVisibilityIcon;
            set => SetProperty(ref _confirmPasswordVisibilityIcon, value);
        }

        public List<int> Levels { get; set; } = new List<int>(Enumerable.Range(1, 40));

        public int SelectedLevel { get; set; }

        public ICommand SignInCommand { get; private set; }
        public ICommand EmailUnfocusedCommand { get; private set; }
        public ICommand NameUnfocusedCommand { get; private set; }
        public ICommand LevelPickedCommand { get; private set; }
        public ICommand ConfirmPasswordUnfocusedCommand { get; set; }
        public ICommand ConfirmPasswordVisibilityToggleCommand { get; private set; }
        #endregion

        public SignInPageViewModel(INavigationService navigationService, IAccountManager accountManager, IDialogManager dialogManager, ICustomLogger logger) 
            : base(navigationService, accountManager, dialogManager, logger) 
        {
            Title = AppResources.SignInPageTitle;

            SignInCommand = new DelegateCommand(async() => await OnSignInButtonClickedAsync());
            EmailUnfocusedCommand = new DelegateCommand(OnEmailUnfocused);
            NameUnfocusedCommand = new DelegateCommand(OnNameUnfocused);
            LevelPickedCommand = new DelegateCommand(OnLevelPicked);
            ConfirmPasswordUnfocusedCommand = new DelegateCommand(OnConfirmPasswordUnfocused);
            ConfirmPasswordVisibilityToggleCommand = new DelegateCommand(OnConfirmPasswordVisibilityToggled);
        }

        private void OnConfirmPasswordVisibilityToggled()
        {
            switch (HideConfirmPassword)
            {
                case true:
                    HideConfirmPassword = false;
                    ConfirmPasswordVisibilityIcon = AppImages.HidePassword;
                    break;

                case false:
                    HideConfirmPassword = true;
                    ConfirmPasswordVisibilityIcon = AppImages.ViewPassword;
                    break;
            }
        }

        private void OnLevelPicked()
        {
            _isLevelValid = true;
            CheckCredentials();
        }

        protected override void InitializeValidatableObjects()
        {
            base.InitializeValidatableObjects();
            Email = new ValidatableObject<string>();
            Name = new ValidatableObject<string>();
            ConfirmPassword = new ValidatableObject<string>();
        }

        protected override void AddValidations()
        {
            base.AddValidations();
            Email.Validations.Add(new IsEmailRule<string>());
            Name.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = AppResources.EmptyNameError });
            ConfirmPassword.Validations.Add(new IsPasswordFormatCorrectRule());
        }

        private async Task OnSignInButtonClickedAsync()
        {
            if (!ValidateEmail() || !ValidateName() || !ValidatePassword() || !ValidateUsername())
            {
                AreCredentialsValid = false;
                return;
            }

            var encryptedPassword = Password.Value.Encrypt("Originals rule");
            var account = new AccountModel(Username.Value, encryptedPassword, Email.Value, Name.Value);
            var trainer = new TrainerModel(Username.Value, SelectedLevel);
            var signInModel = new SignInModels(account, trainer);
            var signInResponse = await _accountManager.SignInAsync(signInModel);

            switch (signInResponse)
            {
                case SignInResponseCode.SignInSuccessful:
                    await NavigationService.NavigateAsync("/" + nameof(NavigationPage)+ "/" + nameof(MainPage));
                    break;

                case SignInResponseCode.EmailAlreadyUsed:
                    Email.Errors = new List<string> { AppResources.EmailAlreadyUsedResponse };
                    Email.IsValid = false;
                    AreCredentialsValid = false;
                    break;

                case SignInResponseCode.UsernameAlreadyRegistered:
                    Username.Errors = new List<string> { AppResources.UserAlreadyRegisteredResponse };
                    Username.IsValid = false;
                    AreCredentialsValid = false;
                    break;

                case SignInResponseCode.PasswordFormatInvalid:
                    Password.Errors = new List<string> { PasswordValidationConstants.PasswordFormatInvalid };
                    Password.IsValid = false;
                    AreCredentialsValid = false;
                    break;

                default:
                    _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
                    break;
            }
        }

        private void OnEmailUnfocused()
        {
            if (string.IsNullOrEmpty(Email.Value))
                return;

            if (Email != null && Email.Value != null)
                Email.Value = Email.Value.Trim();

            _isEmailValid = ValidateEmail();
            CheckCredentials();
        }

        private void OnNameUnfocused()
        {
            if (string.IsNullOrEmpty(Name.Value))
                return;

            if (Name != null && Name.Value != null)
                Name.Value = Name.Value.Trim();

            _isNameValid = ValidateName();
            CheckCredentials();
        }

        private void OnConfirmPasswordUnfocused()
        {
            if (string.IsNullOrEmpty(ConfirmPassword.Value))
                return;

            if (Name != null && Name.Value != null)
                ConfirmPassword.Value = ConfirmPassword.Value.Trim();

            CheckConfirmPasswordMatches();

            
            CheckCredentials();
        }

        private void CheckConfirmPasswordMatches()
        {
            if(ConfirmPassword.Value != Password.Value)
            {
                ConfirmPassword.Errors = new List<string> { AppResources.PasswordsDontMatch };
                ConfirmPassword.IsValid = _isConfirmPasswordValid = false;
            }
            else
            {
                ConfirmPassword.Errors = new List<string>();
                ConfirmPassword.IsValid = _isConfirmPasswordValid = true;
            }
        }

        protected override void OnPaswordUnfocused()
        {
            base.OnPaswordUnfocused();

            if (ConfirmPassword.Value != null)
                CheckConfirmPasswordMatches();
        }

        private bool ValidateEmail()
        {
            return Email.Validate();
        }

        private bool ValidateName()
        {
            return Name.Validate();
        }

        protected override void CheckCredentials()
        {
            AreCredentialsValid = _isEmailValid && _isNameValid && _isUsernameValid && _isPasswordValid && _isLevelValid && _isConfirmPasswordValid;
        }
    }
}
