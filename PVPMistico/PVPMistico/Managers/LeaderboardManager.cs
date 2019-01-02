using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public async Task<CreateLeaderboardResponseCode> CreateLeaderboardAsync(string name, LeagueTypesEnum leagueType, ParticipantModel creator)
        {
            if (creator == null)
                return CreateLeaderboardResponseCode.UnknownError;

            creator.IsAdmin = true;
            var leaderboard = new LeaderboardModel()
            {
                LeagueType = leagueType,
                Name = name,
                Participants = new ObservableCollection<ParticipantModel>()
                {
                    creator
                }
            };
            var response = await _httpManager.PutAsync<CreateLeaderboardResponseCode>(ApiConstants.LeaderboardsURL, leaderboard);

            return response;
        }

        public async Task<LeaderboardModel> GetLeaderboardAsync(int id)
        {
            var response = await _httpManager.GetAsync<LeaderboardModel>(ApiConstants.LeaderboardsURL, ApiConstants.IdExtension, id.ToString());
            if (response == null)
                return null;

            return response;
        }

        public async Task<List<LeaderboardModel>> GetLeaderboardsAsync()
        {
            var response = await _httpManager.GetAsync<List<LeaderboardModel>>(ApiConstants.LeaderboardsURL);
            if (response == null)
                return null;

            return response;
        }

        public async Task<List<LeaderboardModel>> GetMyLeaderboardsAsync(string username)
        {
            var response = await _httpManager.GetAsync<List<LeaderboardModel>>(ApiConstants.LeaderboardsURL, ApiConstants.UsernameExtension, username);
            if (response == null)
                return null;

            return response;
        }

        public async Task<AddTrainerResponseCode> AddTrainerAsync(LeaderboardModel leaderboard, TrainerModel trainer)
        {
            if (trainer == null || leaderboard == null)
                return AddTrainerResponseCode.UnknownError;

            var participant = new ParticipantModel()
            {
                Username = trainer.Username,
                Level = trainer.Level
            };
            var response = await _httpManager.PutAsync<AddTrainerResponseCode>(ApiConstants.LeaderboardsURL, trainer, extension: ApiConstants.TrainerExtension, parameter: leaderboard.ID.ToString());
            return response;
        }

        public bool RemoveTrainer(LeaderboardModel leaderboard, ParticipantModel trainer)
        {
            return leaderboard.Participants.Remove(trainer);
        }

        
        public async Task<bool> InputMatch(LeaderboardModel leaderboard, MatchModel match)
        {
            if (leaderboard == null || match == null)
                return false;

            var response = await _httpManager.PutAsync<bool>(ApiConstants.LeaderboardsURL, match, extension: ApiConstants.MatchExtension, parameter: leaderboard.ID.ToString());

            return response;
        }
    }
}
