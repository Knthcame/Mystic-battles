using Models.Classes;
using Models.Enums;
using System.Collections.Generic;

namespace PVPMistico.Managers.Interfaces
{
    public interface ITournamentManager
    {
        List<LeaderboardModel> GetLeaderboards();

        LeaderboardModel GetLeaderboard(int id);

        bool CreateTournament(string name, LeagueTypesEnum leagueType, ParticipantModel creator);

        IEnumerable<LeaderboardModel> GetMyLeaderboards(string username);

        bool AddTrainer(LeaderboardModel leaderboard, TrainerModel trainer);

        bool RemoveTrainer(LeaderboardModel leaderboard, ParticipantModel participant);
    }
}
