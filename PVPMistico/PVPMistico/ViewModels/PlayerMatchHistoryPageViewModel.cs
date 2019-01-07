using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Enums;
using PVPMistico.Extensions;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Models;
using PVPMistico.Resources;
using PVPMistico.ViewModels.BaseViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class PlayerMatchHistoryPageViewModel : BaseViewModel
    {
        #region Fields
        private TrainerModel _trainer;
        private ObservableCollection<TrainerMatchModel> _matches;
        private LeaderboardModel _leaderboard;
        private bool _didPageLoadSuccesfully = true;
        private bool _isListViewRefreshing;
        private string _trainerUsername;
        private bool _isMatchListEmpty;
        private bool _isMyOwnMatchHistory;
        private readonly IAccountManager _accountManager;
        private readonly IMatchManager _matchManager;
        private readonly IDialogManager _dialogManager;
        private readonly ILeaderboardManager _leaderboardManager;
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

        public bool IsListViewRefreshing
        {
            get => _isListViewRefreshing;
            set => SetProperty(ref _isListViewRefreshing, value);
        }

        public bool IsMatchListEmpty
        {
            get => _isMatchListEmpty;
            set => SetProperty(ref _isMatchListEmpty, value);
        }

        public bool IsMyOwnMatchHistory
        {
            get => _isMyOwnMatchHistory;
            set => SetProperty(ref _isMyOwnMatchHistory, value);
        }

        public ICommand RefreshMatchesCommand { get; }
        public ICommand UndoMatchCommand { get; }
        #endregion

        public PlayerMatchHistoryPageViewModel(INavigationService navigationService, ICustomLogger logger, IAccountManager accountManager,
            IMatchManager matchManager, IDialogManager dialogManager, ILeaderboardManager leaderboardManager)
            : base(navigationService, logger)
        {
            Title = AppResources.MatchHistory;
            _accountManager = accountManager;
            _matchManager = matchManager;
            _dialogManager = dialogManager;
            _leaderboardManager = leaderboardManager;

            IsPageLoading = true;
            RefreshMatchesCommand = new DelegateCommand(async () => await RefreshMatchListAsync());
            UndoMatchCommand = new DelegateCommand<TrainerMatchModel>(async (trainerMatch) => await UndoMatchAsync(trainerMatch));
        }

        private async Task UndoMatchAsync(TrainerMatchModel trainerMatch)
        {
            _dialogManager.ShowLoading(AppResources.Loading);
            var match = await _matchManager.GetMatch(trainerMatch.ID);
            if (match == null)
                _dialogManager.ShowToast(AppResources.Error, ToastModes.Error);
            else
            {
                await _leaderboardManager.UndoMatchAsync(match);
                await RefreshMatchListAsync();
            }
            _dialogManager.EndLoading();
        }

        private async Task RefreshMatchListAsync()
        {
            var matches = await _matchManager.GetPlayerLeagueMatchesAsync(Leaderboard.ID, _trainerUsername);
            if (matches == null)
            {
                IsMatchListEmpty = true;
                return;
            }
            else
                IsMatchListEmpty = matches.Count == 0;

            var trainerMatches = matches.ToTrainerMatchModel(Trainer.Username);
            Matches = new ObservableCollection<TrainerMatchModel>(trainerMatches);
            IsListViewRefreshing = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (!_didPageLoadSuccesfully)
            {
                await NavigationService.GoBackAsync();
                _dialogManager.ShowToast(AppResources.Error, ToastModes.Error);
            }
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (!parameters.TryGetValue(NavigationParameterKeys.UsernameKey, out string username))
            {
                _didPageLoadSuccesfully = false;
                return;
            }

            _trainerUsername = username;

            if (!parameters.TryGetValue(NavigationParameterKeys.LeaderboardKey, out LeaderboardModel leaderboard))
            {
                _didPageLoadSuccesfully = false;
                return;
            }

            var myUsername = await SecureStorage.GetAsync(SecureStorageTokens.Username);
            IsMyOwnMatchHistory = myUsername == username;

            IsListViewRefreshing = true;
            Leaderboard = leaderboard;
            Trainer = await _accountManager.GetTrainer(username);
            await RefreshMatchListAsync();
            IsPageLoading = false;
        }
    }
}
