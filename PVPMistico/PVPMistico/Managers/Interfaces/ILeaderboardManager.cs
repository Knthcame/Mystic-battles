using Models.Classes;
using Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface ILeaderboardManager
    {
        Task<List<LeaderboardModel>> GetLeaderboardsAsync();

        Task<LeaderboardModel> GetLeaderboardAsync(int id);

        Task<CreateLeaderboardResponseCode> CreateTournamentAsync(string name, LeagueTypesEnum leagueType, ParticipantModel creator);

        Task<List<LeaderboardModel>> GetMyLeaderboardsAsync(string username);

        bool AddTrainer(LeaderboardModel leaderboard, TrainerModel trainer);

        bool RemoveTrainer(LeaderboardModel leaderboard, ParticipantModel participant);
        bool InputMatch(LeaderboardModel leaderboard, MatchModel match);
    }
}
