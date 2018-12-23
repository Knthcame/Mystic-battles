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

        public SignInPageViewModel(INavigationService navigationService, IAccountManager accountManager) : base(navigationService, accountManager) 
        {
            Title = "Registro de cuenta";

            SignInCommand = new DelegateCommand(OnSignInButtonClicked);
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
            Username.Validations.Add(new IsUsernameAvailableRule(AccountManager));
            Email.Validations.Add(new IsEmailRule<string>());
            Name.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>() { ValidationMessage = "El nombre no puede estar vacio" });
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
