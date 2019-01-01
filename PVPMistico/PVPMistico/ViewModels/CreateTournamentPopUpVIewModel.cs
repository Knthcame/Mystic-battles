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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class CreateTournamentPopupViewModel : BaseViewModel
    {
        #region Fields
        private readonly ITournamentManager _tournamentManager;
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

        public ICommand CreateTournamentCommand { get; private set; }

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

        public CreateTournamentPopupViewModel(INavigationService navigationService, ICustomLogger logger, ITournamentManager tournamentManager, IAccountManager accountManager, IDialogManager dialogManager)
            : base(navigationService, logger)
        {
            _tournamentManager = tournamentManager;
            _accountManager = accountManager;
            _dialogManager = dialogManager;
            NameUnfocusedComamand = new DelegateCommand(OnNameUnfocused);
            LeagueTypeSelectedCommand = new DelegateCommand(OnLeagueTypeSelected);
            CreateTournamentCommand = new DelegateCommand(async () => await OnCreateTournamentButtonPressedAsync());
            LeagueName = new ValidatableObject<string>();
            LeagueName.Validations.Add(new IsNotNullOrEmptyOrBlankSpaceRule<string>());
            LeagueName.Validations.Add(new IsLeagueNameAvailableRule(_tournamentManager));
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

        private async Task OnCreateTournamentButtonPressedAsync()
        {
            var username = await SecureStorage.GetAsync(SecureStorageTokens.Username);
            var participant = await _accountManager.CreateParticipantAsync(username, isAdmin: true);

            if (participant == null)
                _dialogManager.ShowToast(new ToastConfig(AppResources.Error), ToastModes.Error);
            else
                _tournamentManager.CreateTournament(LeagueName.Value, SelectedLeagueType.LeagueTypesEnum, participant);

            await NavigationService.ClearPopupStackAsync();
        }

        public override bool OnBackButtonPressed()
        {
            NavigationService.ClearPopupStackAsync();
            return true;
        }
    }
}
