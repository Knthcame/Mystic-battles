using PVPMistico.Managers.Interfaces;
using PVPMistico.Resources;
using PVPMistico.Validation.Rules.Interfaces;
using System.Linq;

namespace PVPMistico.Validation.Rules
{
    public class IsLeagueNameAvailableRule : IValidationRule<string>
    {
        private readonly ITournamentManager _tournamentManager;

        public IsLeagueNameAvailableRule(ITournamentManager tournamentManager)
        {
            _tournamentManager = tournamentManager;
        }
        public string ValidationMessage { get; set; } = AppResources.LeagueNameAlreadyUsed;

        public bool Check(string value)
        {
            var leaderboards = _tournamentManager.GetLeaderboards();
            return !leaderboards.Any((leaderboard) => leaderboard.Name == value);
        }
    }
}
