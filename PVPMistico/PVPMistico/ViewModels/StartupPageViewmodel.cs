using System.Threading.Tasks;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.ViewModels.BaseViewModels;
using PVPMistico.Views;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class StartupPageViewmodel : BaseViewModel
    {
        public StartupPageViewmodel(INavigationService navigationService, ICustomLogger logger) 
        : base(navigationService, logger) { }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await CheckAccountStored();
        }
        private async Task CheckAccountStored()
        {
            var username = await SecureStorage.GetAsync(SecureStorageTokens.Username);
            if (username != null)
            {
                _logger.Debug("Registered account username: " + username);
                await NavigationService.NavigateAsync("NavigationPage/" + nameof(MainPage));
            }
            else
                await NavigationService.NavigateAsync("NavigationPage/" + nameof(LogInPage));
        }
    }
}
