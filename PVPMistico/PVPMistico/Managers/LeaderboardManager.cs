using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Models.ApiResponses;
using Models.Classes;
using Models.Enums;
using PVPMistico.Constants;
using PVPMistico.Managers.Interfaces;

namespace PVPMistico.Managers
{
    public class LeaderboardManager : ILeaderboardManager
    {
        private readonly IHttpManager _httpManager;

        private static List<LeaderboardModel> leaderboards = new List<LeaderboardModel>();

        public LeaderboardManager(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<CreateLeaderboardResponse> CreateLeaderboardAsync(string name, LeagueTypesEnum leagueType, TrainerModel creator)
        {
            if (creator == null)
                return new CreateLeaderboardResponse { ResponseCode = CreateLeaderboardResponseCode.UnknownError };

            creator.IsAdmin = true;
            var leaderboard = new LeaderboardModel()
            {
                LeagueType = leagueType,
                Name = name,
                Trainers = new ObservableCollection<TrainerModel>()
                {
                    creator
                }
            };
            var response = await _httpManager.PutAsync<CreateLeaderboardResponse>(ApiConstants.LeaderboardsURL, leaderboard);

            return response;
        }

        public async Task<LeaderboardModel> GetLeaderboardAsync(int id)
        {
            var response = await _httpManager.GetAsync<LeaderboardResponse>(ApiConstants.LeaderboardsURL, ApiConstants.IdExtension, id.ToString());
            if (response == null)
                return null;

            return response.Leaderboard;
        }

        public async Task<List<LeaderboardModel>> GetLeaderboardsAsync()
        {
            var response = await _httpManager.GetAsync<LeaderboardListResponse>(ApiConstants.LeaderboardsURL);
            if (response == null)
                return null;

            return response.Leaderboards;
        }

        public async Task<List<LeaderboardModel>> GetMyLeaderboardsAsync(string username)
        {
            var response = await _httpManager.GetAsync<LeaderboardListResponse>(ApiConstants.LeaderboardsURL, ApiConstants.UsernameExtension, username);
            if (response == null)
                return null;

            return response.Leaderboards;
        }

        public async Task<AddTrainerResponse> AddTrainerAsync(LeaderboardModel leaderboard, TrainerModel trainer)
        { 
            var participant = new TrainerModel()
            {
                Username = trainer.Username,
                Level = trainer.Level,
                Position = leaderboard.Trainers.Count + 1
            };
            var response = await _httpManager.PutAsync<AddTrainerResponse>(ApiConstants.LeaderboardsURL, trainer, extension: ApiConstants.TrainerExtension, parameter: leaderboard.ID.ToString());
            return response;
        }

        public bool RemoveTrainer(LeaderboardModel leaderboard, TrainerModel trainer)
        {
            return leaderboard.Trainers.Remove(trainer);
        }

        
        public async Task<bool> InputMatch(LeaderboardModel leaderboard, MatchModel match)
        {
            if (leaderboard == null || match == null)
                return false;

            var response = await _httpManager.PutAsync<OkResponse>(ApiConstants.LeaderboardsURL, match, extension: ApiConstants.MatchExtension, parameter: leaderboard.ID.ToString());
            if (response == null)
                return false;

            return response.Ok;
        }
    }
}
