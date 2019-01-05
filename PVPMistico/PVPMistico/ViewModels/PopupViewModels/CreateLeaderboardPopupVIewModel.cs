using Acr.UserDialogs;
using Models.Enums;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Dictionaries;
using PVPMistico.Enums;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Models;
using PVPMistico.Resources;
using PVPMistico.Validation;
using PVPMistico.Validation.Rules;
using PVPMistico.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels.PopupViewModels
{
    public class CreateLeaderboardPopupViewModel : BaseViewModel
    {
        #region Fields
        private readonly ILeaderboardManager _leaderboardManager;
        private readonly IAccountManager _accountManager;
        private readonly IDialogManager _dialogManager;
        private bool _buttonEnabled;
        private ValidatableObject<string> _leagueName;
        private bool _isLeagueNameValid;
        private bool _isLeagueTypeValid;
        #endregion

        #region Properties
        public List<LeagueTypePickerItemModel> LeagueTypes { get; set; } = CreateLeagueTypesList();

        public LeagueTypePickerItemModel SelectedLeagueType { get; set; }

        public ICommand CreateLeaderboardCommand { get; private set; }

        public ICommand NameUnfocusedComamand { get; private set; }

        public ICommand LeagueTypeSelectedCommand { get; private set; }

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => SetProperty(ref _buttonEnabled, value);
        }

        public ValidatableObject<string> LeagueName
        {
            get => _leagueName;
            set => SetProperty(ref _leagueName, value);
        }
        #endregion

        public CreateLeaderboardPopupViewModel(INavigationService navigationService, ICustomLogger logger, ILeaderboardManager leaderboardManager, IAccountManager accountManager, IDialogManager dialogManager)
            : base(navigationService, logger)
        {
            _leaderboardManager = leaderboardManager;
            _accountManager = accountManager;
            _dialogManager = dialogManager;
            NameUnfocusedComamand = new DelegateCommand(OnNameUnfocused);
            LeagueTypeSelectedCommand = new DelegateCommand(OnLeagueTypeSelected);
            CreateLeaderboardCommand = new DelegateCommand(async () => await OnCreateLeaderboardButtonPressedAsync());
            LeagueName = new ValidatableObject<string>();
            LeagueName.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>());
        }

        private void OnLeagueTypeSelected()
        {
            _isLeagueTypeValid = SelectedLeagueType != null;
            CheckValidations();
        }

        private void OnNameUnfocused()
        {
            if (LeagueName != null && LeagueName.Value != null)
                LeagueName.Value = LeagueName.Value.Trim();

            if (string.IsNullOrEmpty(LeagueName.Value))
                return;

            _isLeagueNameValid = LeagueName.Validate();
            CheckValidations();
        }

        private void CheckValidations()
        {
            ButtonEnabled = _isLeagueNameValid && _isLeagueTypeValid;
        }

        private static List<LeagueTypePickerItemModel> CreateLeagueTypesList()
        {
            var enums = Enum.GetValues(typeof(LeagueTypesEnum));
            var list = new List<LeagueTypePickerItemModel>();
            foreach (LeagueTypesEnum leagueTypeEnum in enums)
            {
                if (LeagueTypesDictionary.GetLeagueTypeString(leagueTypeEnum, out string leagueTypeName))
                    list.Add(new LeagueTypePickerItemModel(leagueTypeEnum, leagueTypeName));
            }

            return list;
        }

        private async Task OnCreateLeaderboardButtonPressedAsync()
        {
            _dialogManager.ShowLoading();
            var username = await SecureStorage.GetAsync(SecureStorageTokens.Username);
            var participant = await _accountManager.CreateParticipantAsync(username, isAdmin: true);

            if (participant == null)
            {
                _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
                return;
            }
            
            var response = await _leaderboardManager.CreateLeaderboardAsync(LeagueName.Value, SelectedLeagueType.LeagueTypesEnum, participant);
            _dialogManager.EndLoading();
            switch(response)
            {
                case CreateLeaderboardResponseCode.CreatedSuccessfully:
                    await NavigationService.ClearPopupStackAsync();
                    break;

                case CreateLeaderboardResponseCode.NameAlreadyUsed:
                    LeagueName.Errors = new List<string> { AppResources.LeagueNameAlreadyUsed };
                    LeagueName.IsValid = false;
                    break;
                default:
                    _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
                    break;
            }
        }

        public override bool OnBackButtonPressed()
        {
            NavigationService.ClearPopupStackAsync();
            return true;
        }
    }
}
