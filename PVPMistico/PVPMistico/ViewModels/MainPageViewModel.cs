using Acr.UserDialogs;
using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Views;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _menuText;
        private IAccountManager _accountManager;
        private IDialogManager _dialogManager;
        private readonly ITournamentManager _tournamentManager;

        public string MenuText
        {
            get => _menuText;
            set => SetProperty(ref _menuText, value);
        }

        public ObservableCollection<LeaderBoardPreviewModel> LeaderboardPreviews { get; set; }

        public DelegateCommand MenuItemCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, IAccountManager accountManager, ICustomLogger logger, IDialogManager dialogManager, ITournamentManager tournamentManager)
            : base(navigationService, logger)
        {
            _accountManager = accountManager;
            _dialogManager = dialogManager;
            _tournamentManager = tournamentManager;
            Title = AppResources.MainPageTitle;
            MenuText = AppResources.LogOutButtonText;
            MenuItemCommand = new DelegateCommand(OnLogOutClicked);
            LeaderboardPreviews = LoadMyLeaderboards();
        }

        private ObservableCollection<LeaderBoardPreviewModel> LoadMyLeaderboards()
        {
            var usernameTask = SecureStorage.GetAsync(SecureStorageTokens.Username);
            var username = usernameTask.Result;
            var leaderboards = _tournamentManager.GetMyLeaderboards(username);

            var leaderboardPreviews = new ObservableCollection<LeaderBoardPreviewModel>();

            foreach(LeaderboardModel leaderboard in leaderboards)
            {
                var leaderboardPreview = new LeaderBoardPreviewModel()
                {
                    ID = leaderboard.ID,
                    LeagueType = leaderboard.LeagueType,
                    Name = leaderboard.Name,
                    Participant = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == username)
                };
                leaderboardPreviews.Add(leaderboardPreview);
            }

            return leaderboardPreviews;
        }

        private void OnLogOutClicked()
        {
            var config = new ConfirmConfig()
            {
                Title = AppResources.LogOutConfirmationTitle,
                Message = AppResources.LogOutConfirmationMessage,
                OkText = AppResources.ConfirmationDialogOkButton,
                CancelText = AppResources.ConfirmationDialogCancelButton,
                OnAction = OnLogOutAction
            };
            _dialogManager.ShowConfirmationDialog(config);
        }

        private async void OnLogOutAction(bool confirmed)
        {
            if (confirmed)
            {
                _accountManager.LogOut();
                await NavigationService.NavigateAsync("/NavigationPage/" + nameof(LogInPage));
            }
        }
    }
}
