using Models.Classes;
using Models.Enums;
using Prism.Commands;
using Prism.Navigation;
using PVPMistico.Constants;
using PVPMistico.Dictionaries;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace PVPMistico.ViewModels
{
    public class CreateTournamentPopUpVIewModel : BaseViewModel
    {
        private readonly ITournamentManager _tournamentManager;
        private readonly IAccountManager _accountManager;

        public List<LeagueTypePickerItemModel> LeagueTypes { get; set; } = CreateLeagueTypesList();

        public string LeagueName { get; set; }

        public LeagueTypePickerItemModel SelectedLeagueType { get; set; }

        public ICommand CreateTournamentCommand { get; private set; }

        public CreateTournamentPopUpVIewModel(INavigationService navigationService, ICustomLogger logger, ITournamentManager tournamentManager, IAccountManager accountManager)
            : base(navigationService, logger)
        {
            _tournamentManager = tournamentManager;
            _accountManager = accountManager;
            CreateTournamentCommand = new DelegateCommand(async() => await OnCreateTournamentButtonPressedAsync());
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
            var participant =_accountManager.CreateParticipant(username);
            _tournamentManager.CreateTournament(LeagueName, SelectedLeagueType.LeagueTypesEnum, participant);
            await NavigationService.ClearPopupStackAsync();
        }

        public override bool OnBackButtonPressed()
        {
            NavigationService.ClearPopupStackAsync();
            return true;
        }
    }
}
