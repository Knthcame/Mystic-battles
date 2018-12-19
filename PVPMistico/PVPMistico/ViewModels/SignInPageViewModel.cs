using System;
using Prism.Commands;
using Prism.Navigation;

namespace PVPMistico.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        #region Fields
        private string _password;
        private string _passwordVisibilityIcon = "PasswordVisible.png";
        private bool _hidePassword = true;
        #endregion

        #region Properties
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

        public DelegateCommand PasswordVisibilityToggleCommand { get; private set; }
        #endregion

        public SignInPageViewModel(INavigationService navigationService) : base(navigationService) 
        {
            Title = "Registro de cuenta";

            PasswordVisibilityToggleCommand = new DelegateCommand(OnPasswordVisibilityToggle);
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
    }
}
