using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
using PVPMistico.Views;

namespace PVPMistico.ViewModels
{
    public class SignInPageViewModel : BaseAccountValidationViewModel
    {
        #region Fields
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _email;
        private bool _isEmailValid;
        private bool _isNameValid;
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

        public ICommand SignInCommand { get; private set; }
        public ICommand EmailUnfocusedCommand { get; private set; }
        public ICommand NameUnfocusedCommand { get; private set; }
        #endregion

        public SignInPageViewModel(INavigationService navigationService, IAccountManager accountManager, IDialogManager dialogManager, ICustomLogger logger) 
            : base(navigationService, accountManager, dialogManager, logger) 
        {
            Title = AppResources.SignInPageTitle;

            SignInCommand = new DelegateCommand(async() => await OnSignInButtonClickedAsync());
            EmailUnfocusedCommand = new DelegateCommand(OnEmailUnfocused);
            NameUnfocusedCommand = new DelegateCommand(OnNameUnfocused);
        }

        protected override void InitializeValidatableObjects()
        {
            base.InitializeValidatableObjects();
            Email = new ValidatableObject<string>();
            Name = new ValidatableObject<string>();
        }

        protected override void AddValidations()
        {
            base.AddValidations();
            Email.Validations.Add(new IsEmailRule<string>());
            Name.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = AppResources.EmptyNameError });
        }

        private async Task OnSignInButtonClickedAsync()
        {
            if (!ValidateEmail() || !ValidateName())
                return;

            var account = new AccountModel(Username.Value, Password.Value, Email.Value, Name.Value);
            var signInResponse = await _accountManager.SignInAsync(account);

            if (signInResponse == SignInResponses.SignInSuccessful)
                await NavigationService.NavigateAsync("/NavigationPage/" + nameof(MainPage));

            _dialogManager.ShowToast(new ToastConfig(signInResponse));
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
            AreCredentialsValid = _isEmailValid && _isNameValid && _isUsernameValid && _isPasswordValid;
        }
    }
}
