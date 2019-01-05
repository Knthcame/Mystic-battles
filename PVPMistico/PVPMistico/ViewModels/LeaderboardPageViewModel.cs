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
using PVPMistico.Views.Popups;
using PVPMistico.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using System;

namespace PVPMistico.ViewModels
{
    public class LeaderboardPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly ILeaderboardManager _leaderboardManager;
        private readonly IDialogManager _dialogManager;
        private LeaderboardModel _leaderboard;
        private bool _isCurrentUserAdmin;
        private ObservableCollection<ParticipantModel> _participants;
        private ParticipantModel _selectedParticipant;
        private bool _isLeaderboardRefreshing;
        private ParticipantModel _participantToDelete;
        #endregion

        #region Properties
        public LeaderboardModel Leaderboard
        {
            get => _leaderboard;
            set => SetProperty(ref _leaderboard, value);
        }

        public ObservableCollection<ParticipantModel> Participants
        {
            get => _participants;
            set => SetProperty(ref _participants, value);
        }

        public bool IsCurrentUserAdmin
        {
            get => _isCurrentUserAdmin;
            set => SetProperty(ref _isCurrentUserAdmin, value);
        }

        public bool IsLeaderboardRefreshing
        {
            get => _isLeaderboardRefreshing;
            set => SetProperty(ref _isLeaderboardRefreshing, value);
        }

        public ParticipantModel SelectedParticipant
        {
            get => _selectedParticipant;
            set => SetProperty(ref _selectedParticipant, value);
        }

        public ICommand AddTrainerCommand { get; private set; }
        public ICommand InputMatchCommand { get; private set; }
        public ICommand ParticipantSelectedCommand { get; private set; }
        public ICommand ChangeAdminPermissionsCommand { get; private set; }
        public ICommand RefreshParticipantsCommand { get; private set; }
        public ICommand DeleteParticipantCommand { get; private set; }
        #endregion

        public LeaderboardPageViewModel(INavigationService navigationService, ICustomLogger logger, ILeaderboardManager leaderboardManager, IDialogManager dialogManager)
            : base(navigationService, logger)
        {
            _leaderboardManager = leaderboardManager;
            _dialogManager = dialogManager;
            IsPageLoading = true;
            AddTrainerCommand = new DelegateCommand(async () => await OnAddTrainerButtonPressedAsync());
            InputMatchCommand = new DelegateCommand(async () => await OnInputMatchButtonPressedAsync());
            ParticipantSelectedCommand = new DelegateCommand(async () => await OnSelectParticipantAsync());
            ChangeAdminPermissionsCommand = new DelegateCommand<ParticipantModel>(async (participant) => await ChangeAdminPermissions(participant));
            RefreshParticipantsCommand = new DelegateCommand(async () => await RefreshParticipants());
            DeleteParticipantCommand = new DelegateCommand<ParticipantModel>(AskForParticipantRemovalConfirmation);
        }

        private void AskForParticipantRemovalConfirmation(ParticipantModel participant)
        {
            _participantToDelete = participant;
            var config = new ConfirmConfig
            {
                OkText = AppResources.ConfirmationDialogOkButton,
                CancelText = AppResources.ConfirmationDialogCancelButton,
                Title = AppResources.DeleteParticipant,
                Message = AppResources.DeleteParticipantConfirmationMessage + "\n\n" + participant.Username,
                OnAction = async (confirmed) => await DeleteParticipantAsync(confirmed)
            };
            _dialogManager.ShowConfirmationDialog(config);
        }

        private async Task DeleteParticipantAsync(bool confirmed)
        {
            if (!confirmed || _participantToDelete == null)
                return;

            _dialogManager.ShowLoading(AppResources.Loading);
            var succes = await _leaderboardManager.RemoveTrainerAsync(Leaderboard, _participantToDelete);
            _dialogManager.EndLoading();
            if (succes)
                await RefreshParticipants();
            else
                _dialogManager.ShowToast(AppResources.Error, ToastModes.Error);
        }

        private async Task RefreshParticipants()
        {
            IsLeaderboardRefreshing = true;
            await LoadLeaderboardAsync(Leaderboard.ID);
            Participants = Leaderboard.Participants;
            OrderParticipants();
            IsLeaderboardRefreshing = false;
        }

        private async Task ChangeAdminPermissions(ParticipantModel participant)
        {
            _dialogManager.ShowLoading(AppResources.Loading);
            if (participant == null)
                return;

            participant.IsAdmin ^= true;
            var success = await _leaderboardManager.UpdateParticipantAsync(Leaderboard, participant);

            _dialogManager.EndLoading();
            if (success)
                await RefreshParticipants();
            else
                _dialogManager.ShowToast(AppResources.Error, ToastModes.Error);
        }

        private async Task OnSelectParticipantAsync()
        {
            if (SelectedParticipant == null)
                return;

            var parameters = new NavigationParameters
            {
                {NavigationParameterKeys.UsernameKey, SelectedParticipant.Username },
                {NavigationParameterKeys.LeaderboardKey, Leaderboard }
            };
            await NavigationService.NavigateAsync(nameof(PlayerMatchHistoryPage), parameters);
            SelectedParticipant = null;
        }

        private async Task OnInputMatchButtonPressedAsync()
        {
            var parameters = new NavigationParameters()
            {
                {NavigationParameterKeys.LeaderboardKey, Leaderboard }
            };
            await NavigationService.NavigateAsync(nameof(InputMatchPopup), parameters);
        }

        private async Task OnAddTrainerButtonPressedAsync()
        {
            var parameters = new NavigationParameters()
            {
                {NavigationParameterKeys.LeaderboardKey, Leaderboard }
            };
            await NavigationService.NavigateAsync(nameof(AddTrainerPopup), parameters);
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.TryGetValue(NavigationParameterKeys.LeaderboardNameKey, out string name))
                Leaderboard = new LeaderboardModel { Name = name };

            if (parameters.TryGetValue(NavigationParameterKeys.LeaderboardIdKey, out int id))
                await LoadLeaderboardAsync(id);

            else if (Leaderboard == null)
            {
                await NavigationService.GoBackAsync();
                _dialogManager.ShowToast(new ToastConfig(AppResources.LeaderboardNotFoundToast), ToastModes.Error);
                return;
            }
            else
                await RefreshParticipants();
            OrderParticipants();
            await SetAdminPermissionAsync();
            IsPageLoading = false;
        }

        private async Task LoadLeaderboardAsync(int id)
        {
            Leaderboard = await _leaderboardManager.GetLeaderboardAsync(id);
        }

        private void OrderParticipants()
        {
            var orderedParticipant = Leaderboard.Participants.OrderBy((participant) => participant.Position);
            Participants = new ObservableCollection<ParticipantModel>(orderedParticipant);
        }

        private async Task SetAdminPermissionAsync()
        {
            var myUsername = await SecureStorage.GetAsync(SecureStorageTokens.Username);

            var currentUser = Leaderboard.Participants.FirstOrDefault((trainer) => trainer.Username == myUsername);
            if (currentUser != null && currentUser.IsAdmin == true)
                IsCurrentUserAdmin = true;
        }
    }
}
