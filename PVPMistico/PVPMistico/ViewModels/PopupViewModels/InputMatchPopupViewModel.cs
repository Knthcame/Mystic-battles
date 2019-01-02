using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Enums;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.ViewModels.BaseViewModels;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels.PopupViewModels
{
    public class InputMatchPopupViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDialogManager _dialogManager;
        private readonly ILeaderboardManager _leaderboardManager;
        private LeaderboardModel _leaderboard;
        private MatchModel _match;
        private ObservableCollection<TrainerModel> _opponentsList;
        private ObservableCollection<TrainerModel> _competitorsList;
        private TrainerModel _currentUser;
        private bool _isOpponentSelected;
        private bool _isWinnerSelected;
        private bool _isOpponentPickerEnabled;
        #endregion

        #region Properties
        public TrainerModel SelectedOpponent { get; set; }

        public TrainerModel SelectedWinner { get; set; }

        public ICommand OpponentSelectedCommand { get; private set; }

        public ICommand WinnerSelectedCommand { get; private set; }

        public ICommand ConfimInputCommand { get; private set; }

        public bool IsOpponentSelected
        {
            get => _isOpponentSelected;
            set => SetProperty(ref _isOpponentSelected, value);
        }

        public bool IsOpponentPickerEnabled
        {
            get => _isOpponentPickerEnabled;
            set => SetProperty(ref _isOpponentPickerEnabled, value);
        }

        public bool IsWinnerSelected
        {
            get => _isWinnerSelected;
            set => SetProperty(ref _isWinnerSelected, value);
        }

        public ObservableCollection<TrainerModel> PossibleOpponentsList
        {
            get => _opponentsList;
            set => SetProperty(ref _opponentsList, value);
        }

        public ObservableCollection<TrainerModel> Rivals
        {
            get => _competitorsList;
            set => SetProperty(ref _competitorsList, value);
        }
        #endregion

        public InputMatchPopupViewModel(INavigationService navigationService, ICustomLogger logger, IDialogManager dialogManager, ILeaderboardManager leaderboardManager) 
            : base(navigationService, logger)
        {
            _dialogManager = dialogManager;
            _leaderboardManager = leaderboardManager;
            OpponentSelectedCommand = new DelegateCommand(OnOpponentSelected);
            WinnerSelectedCommand = new DelegateCommand(OnWinnerSelected);
            ConfimInputCommand = new DelegateCommand(async() => await ConfirmInputAsync());
        }

        private async Task ConfirmInputAsync()
        {
            if (await _leaderboardManager.InputMatch(_leaderboard, _match))
            {
                await NavigationService.GoBackAsync();
            }
            else
                _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
        }

        private void OnWinnerSelected()
        {
            IsWinnerSelected = true;
            _match.Winner = SelectedWinner.Username;
            _match.Loser = Rivals.FirstOrDefault((opponent) => opponent != SelectedWinner).Username;
        }

        private void OnOpponentSelected()
        {
            IsOpponentSelected = true;
            SetCompetitors();
        }

        private void InitializePickers()
        {
            IsOpponentPickerEnabled = true;
            var opponentsList = new List<TrainerModel>(_leaderboard.Trainers);
            opponentsList.Remove(_currentUser);
            PossibleOpponentsList = new ObservableCollection<TrainerModel>(opponentsList);
        }

        private void InitializaMatch()
        {
            _match = new MatchModel()
            {
                League = _leaderboard.Name,
                LeagueType = _leaderboard.LeagueType
            };
        }

        private void SetCompetitors()
        {
            if (SelectedOpponent == null)
            {
                IsOpponentSelected = false;
                return;
            }
            
            Rivals = new ObservableCollection<TrainerModel>()
            {
                _currentUser,
                SelectedOpponent
            };
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (!parameters.TryGetValue(NavigationParameterKeys.LeaderboardKey, out _leaderboard))
            {
                _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
                await NavigationService.GoBackAsync();
                return;
            }
            var currentUsername = await SecureStorage.GetAsync(SecureStorageTokens.Username);
            _currentUser = _leaderboard.Trainers.First((participant) => participant.Username == currentUsername);

            InitializePickers();

            InitializaMatch();
        }
    }
}
