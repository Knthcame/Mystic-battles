using Models.Classes;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PVPService.Services
{
    public class LeaderboardsRepository
    {
        private readonly Database _database = new Database();

        private readonly BlobsManager _blobsManager = new BlobsManager();

        public List<LeaderboardModel> GetLeaderboards() 
            => _blobsManager.DeblobLeaderboards(_database.GetLeaderboards());

        public List<LeaderboardModel> GetUserLeaderBoards(string username)
        {
            var leaderborads = _database.GetLeaderboards();
            leaderborads = _blobsManager.DeblobLeaderboards(leaderborads);
                
            return leaderborads.Where((boards) => boards.Participants.Any((participant) => participant.Username == username)).ToList();
        }


        public LeaderboardModel GetLeaderboard(int id)
        {
            var leaderboard =_database.GetLeaderboards().Find((board) => board.ID == id);
            leaderboard = _blobsManager.DeblobLeaderboard(leaderboard);
            return leaderboard;
        }
            

        public bool InputMatch(int id, MatchModel match)
        {
            if (match == null)
                return false;

            var leaderboard = GetLeaderboard(id);
            if (leaderboard == null)
                return false;

            var winner = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == match.Winner.Username);
            var loser = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == match.Loser.Username);

            if (winner == null || loser == null)
                return false;

            match.DateTime = DateTime.Now;

            AddWin(winner);
            AddLoss(loser);
            RecalculatePositions(leaderboard);

            _database.AddMatch(match);
            _database.UpdateLeaderboard(leaderboard);
            return true;
        }

        public CreateLeaderboardResponseCode AddLeaderboard(LeaderboardModel leaderboard)
        {
            if (leaderboard == null || leaderboard.Participants == null)
                return CreateLeaderboardResponseCode.UnknownError;

            if (_database.GetLeaderboards().Any(board => board.Name == leaderboard.Name))
                return CreateLeaderboardResponseCode.NameAlreadyUsed;
            
            if (_database.AddLeaderboard(leaderboard))
                return CreateLeaderboardResponseCode.CreatedSuccessfully;
            else
                return CreateLeaderboardResponseCode.UnknownError;
        }

        public AddTrainerResponseCode AddTrainer(int leaderboardId, ParticipantModel trainer)
        {
            if (trainer == null)
                return AddTrainerResponseCode.UnknownError;

            var leaderboard = GetLeaderboard(leaderboardId);
            if (leaderboard == null)
                return AddTrainerResponseCode.UnknownError;

            if (leaderboard.Participants.Any(participant => participant.Username == trainer.Username))
                return AddTrainerResponseCode.TrainerAlreadyParticipates;

            trainer.Position = leaderboard.Participants.Count + 1;

            leaderboard.Participants.Add(trainer);
            if(_database.UpdateLeaderboard(leaderboard))
                return AddTrainerResponseCode.TrainerAddedSuccesfully;
            else
                return AddTrainerResponseCode.UnknownError;
        }

        private int GenerateNewID()
        {
            int newId;
            do
            {
                newId = new Random().Next();
            }
            while (_database.GetLeaderboards().Any((board => board.ID == newId)));

            return newId;
        }

        private void RecalculatePositions(LeaderboardModel leaderboard)
        {
            var orderedParticipants = leaderboard.Participants.OrderByDescending((participant) => participant.Points);
            int i = 1;
            foreach (ParticipantModel participant in orderedParticipants)
            {
                participant.Position = i++;
            }
        }

        private void AddLoss(ParticipantModel loser)
        {
            loser.Losses++;
        }

        private void AddWin(ParticipantModel winner)
        {
            winner.Wins++;
            winner.Points += 3;
        }
    }
}
