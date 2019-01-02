﻿using Models.ApiResponses;
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

        Task<CreateLeaderboardResponse> CreateLeaderboardAsync(string name, LeagueTypesEnum leagueType, ParticipantModel creator);

        Task<List<LeaderboardModel>> GetMyLeaderboardsAsync(string username);

        Task<AddTrainerResponse> AddTrainerAsync(LeaderboardModel leaderboard, TrainerModel trainer);

        bool RemoveTrainer(LeaderboardModel leaderboard, ParticipantModel trainer);

        Task<bool> InputMatch(LeaderboardModel leaderboard, MatchModel match);
    }
}
