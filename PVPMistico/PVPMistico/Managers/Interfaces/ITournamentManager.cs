using Models.Classes;
using System.Collections.Generic;

namespace PVPMistico.Managers.Interfaces
{
    public interface ITournamentManager
    {
        List<LeaderboardModel> GetLeaderboards();

        LeaderboardModel GetLeaderboard(int id);

        LeaderboardModel CreateTournament();

        IEnumerable<LeaderboardModel> GetMyLeaderboards(string username);
    }
}
