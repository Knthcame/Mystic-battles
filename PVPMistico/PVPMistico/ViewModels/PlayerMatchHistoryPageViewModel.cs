using Models.Classes;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Extensions;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Models;
using PVPMistico.Resources;
using PVPMistico.ViewModels.BaseViewModels;
using System.Collections.ObjectModel;

namespace PVPMistico.ViewModels
{
    public class PlayerMatchHistoryPageViewModel : BaseViewModel
    {
        #region Fields
        private TrainerModel _trainer;
        private ObservableCollection<TrainerMatchModel> _matches;
        private LeaderboardModel _leaderboard;
        private readonly IAccountManager _accountManager;
        private readonly IMatchManager _matchManager;
        #endregion

        #region Properties
        public TrainerModel Trainer
        {
            get => _trainer;
            set => SetProperty(ref _trainer, value);
        }

        public ObservableCollection<TrainerMatchModel> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        public LeaderboardModel Leaderboard
        {
            get => _leaderboard;
            set => SetProperty(ref _leaderboard, value);
        }
        #endregion

        public PlayerMatchHistoryPageViewModel(INavigationService navigationService, ICustomLogger logger, IAccountManager accountManager, IMatchManager matchManager) 
            : base(navigationService, logger)
        {
            Title = AppResources.MatchHistory;
            _accountManager = accountManager;
            _matchManager = matchManager;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if(!parameters.TryGetValue(NavigationParameterKeys.UsernameKey, out string username))
                await NavigationService.GoBackAsync();

            if (!parameters.TryGetValue(NavigationParameterKeys.LeaderboardKey, out LeaderboardModel leaderboard))
                await NavigationService.GoBackAsync();

            Leaderboard = leaderboard;
            Trainer = await _accountManager.GetTrainer(username);
            var matches = await _matchManager.GetPlayerLeagueMatchesAsync(leaderboard.ID, username);
            var trainerMatches = matches.ToTrainerMatchModel(Trainer.Username);
            Matches = new ObservableCollection<TrainerMatchModel>(trainerMatches);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }
    }
}
