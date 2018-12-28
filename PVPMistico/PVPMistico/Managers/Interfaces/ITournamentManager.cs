using Models.Classes;
using Models.Enums;
using System.Collections.Generic;

namespace PVPMistico.Managers.Interfaces
{
    public interface ITournamentManager
    {
        List<LeaderboardModel> GetLeaderboards();

        LeaderboardModel GetLeaderboard(int id);

        bool CreateTournament(string name, LeagueTypesEnum leagueType);

        IEnumerable<LeaderboardModel> GetMyLeaderboards(string username);
    }
}
