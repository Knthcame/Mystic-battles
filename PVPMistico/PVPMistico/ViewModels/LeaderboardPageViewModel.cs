using Acr.UserDialogs;
using Models.Classes;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using System.Linq;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class LeaderboardPageViewModel : BaseViewModel
    {
        private readonly ITournamentManager _tournamentManager;
        private readonly IDialogManager _dialogManager;
        private LeaderboardModel _leaderboard;
        private bool _isCurrentUserAdmin;

        public LeaderboardModel Leaderboard
        {
            get => _leaderboard;
            set => SetProperty(ref _leaderboard, value);
        }

        public bool IsCurrentUserAdmin
        {
            get => _isCurrentUserAdmin;
            set => SetProperty(ref _isCurrentUserAdmin, value);
        }

        public LeaderboardPageViewModel(INavigationService navigationService, ICustomLogger logger, ITournamentManager tournamentManager, IDialogManager dialogManager)
            : base(navigationService, logger)
        {
            _tournamentManager = tournamentManager;
            _dialogManager = dialogManager;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.TryGetValue(NavigationParameterKeys.LeaderboardIdKey, out int id))
                Leaderboard = _tournamentManager.GetLeaderboard(id);
            else
            {
                await NavigationService.GoBackAsync();
                _dialogManager.ShowToast(new ToastConfig(AppResources.LeaderboardNotFoundToast));
                return;
            }

            var usernameTask = SecureStorage.GetAsync(SecureStorageTokens.Username);
            var username = usernameTask.Result;

            var currentUser = Leaderboard.Participants.FirstOrDefault((trainer) => trainer.Username == username);
            if (currentUser != null && currentUser.IsAdmin == true)
                IsCurrentUserAdmin = true;

        }
    }
}
