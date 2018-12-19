using System;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Views;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _menuText;

        public string MenuText
        {
            get => _menuText;
            set => SetProperty(ref _menuText, value);
        }

        public DelegateCommand MenuItemCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            MenuText = "Cerrar sesión";
            MenuItemCommand = new DelegateCommand(OnLogOutClicked);
        }

        private void OnLogOutClicked()
        {
            var confirmConfig = new ConfirmConfig()
            {
                Title = "Cerrando sesión",
                Message = "Estás seguro de que quieres cerrar sesión?",
                OkText = "Si, seguro",
                CancelText = "No, cancelar",
                OnAction = OnLogOutAction
            };
            UserDialogs.Instance.Confirm(confirmConfig);
        }

        private void OnLogOutAction(bool confirmed)
        {
            if (confirmed)
            {
                SecureStorage.Remove(SecureStorageTokens.Username);
                NavigationService.NavigateAsync("/NavigationPage/" + nameof(LogInPage));
            }
        }
    }
}
