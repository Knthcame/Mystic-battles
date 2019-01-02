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

        public async Task<CreateLeaderboardResponseCode> CreateTournamentAsync(string name, LeagueTypesEnum leagueType, ParticipantModel creator)
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
            var response = await _httpManager.PutAsync<CreateLeaderboardResponse>(ApiConstants.LeaderboardsURL, leaderboard);
            if (response == null)
                return CreateLeaderboardResponseCode.UnknownError;

            return response.ResponseCode;
        }

        public async Task<LeaderboardModel> GetLeaderboardAsync(int id)
        {
            var response = await _httpManager.GetAsync<LeaderboardResponse>(ApiConstants.LeaderboardsURL + ApiConstants.IdExtension + id);
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
            var response = await _httpManager.GetAsync<LeaderboardListResponse>(ApiConstants.LeaderboardsURL + ApiConstants.UsernameExtension + username);
            if (response == null)
                return null;

            return response.Leaderboards;
        }

        public bool AddTrainer(LeaderboardModel leaderboard, TrainerModel trainer)
        {
            if (TrainerAlreadyParticipates(leaderboard, trainer))
                return false;

            var participant = new ParticipantModel()
            {
                Username = trainer.Username,
                Level = trainer.Level,
                Position = leaderboard.Participants.Count + 1
            };
            leaderboard.Participants.Add(participant);
            return true;
        }

        public bool RemoveTrainer(LeaderboardModel leaderboard, ParticipantModel participant)
        {
            return leaderboard.Participants.Remove(participant);
        }

        private bool TrainerAlreadyParticipates(LeaderboardModel leaderboard, TrainerModel trainer)
        {
            var matchingUsernames = leaderboard.Participants.Where((participant) => participant.Username == trainer.Username);
            return matchingUsernames.Count() > 0;
        }

        
        public bool InputMatch(LeaderboardModel leaderboard, MatchModel match)
        {
            if (leaderboard == null || match == null)
                return false;

            match.DateTime = DateTime.Now;

            var winner = leaderboard.Participants.FirstOrDefault((participant => participant.Username == match.Winner));
            winner.Wins++;
            winner.Points += 3;
            winner.Matches.Add(match);

            var loser = leaderboard.Participants.FirstOrDefault((participant => participant.Username == match.Loser));
            loser.Losses++;
            loser.Matches.Add(match);

            return true;
        }
    }
}
