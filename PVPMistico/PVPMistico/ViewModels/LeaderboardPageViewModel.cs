using Acr.UserDialogs;
using Models.Classes;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;

namespace PVPMistico.ViewModels
{
    public class LeaderboardPageViewModel : BaseViewModel
    {
        private readonly ITournamentManager _tournamentManager;
        private readonly IDialogManager _dialogManager;
        private LeaderboardModel _leaderboard;

        public LeaderboardModel Leaderboard
        {
            get => _leaderboard;
            set => SetProperty(ref _leaderboard, value);
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
            }

        }
    }
}
