using Models.Classes;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (match == null || match.Winner == null || match.Loser == null)
                return false;

            var leaderboard = GetLeaderboard(id);
            if (leaderboard == null)
                return false;

            leaderboard = _blobsManager.DeblobLeaderboard(leaderboard);
            var winner = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == match.Winner.Username);
            var loser = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == match.Loser.Username);

            if (winner == null || loser == null)
                return false;

            match.DateTime = DateTime.Now;

            leaderboard = RecalculatePoints(leaderboard, winner, loser);
            leaderboard = RecalculatePositions(leaderboard);

            _database.AddMatch(match);
            _database.UpdateLeaderboard(leaderboard);
            return true;
        }

        public bool UpdateParticipant(int leaderboardId, ParticipantModel trainer)
        {
            if (trainer == null)
                return false;

            var leaderboard = GetLeaderboard(leaderboardId);
            if (leaderboard == null)
                return false;

            var player = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == trainer.Username);
            if (player == null)
                return false;

            var index = leaderboard.Participants.IndexOf(player);
            leaderboard.Participants[index] = trainer;
            return _database.UpdateLeaderboard(leaderboard);
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

        public bool RemoveTrainer(int leaderboardId, string username)
        {
            var leaderboard = GetLeaderboard(leaderboardId);
            if (leaderboard == null)
                return false;

            var trainer = leaderboard.Participants.FirstOrDefault(participant => participant.Username == username);
            var succes = leaderboard.Participants.Remove(trainer);

            if (succes)
            {
                leaderboard = RecalculatePositions(leaderboard);
                _database.UpdateLeaderboard(leaderboard);
            }
            
            return succes;
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

            leaderboard = RecalculatePositions(leaderboard);

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

        private LeaderboardModel RecalculatePositions(LeaderboardModel leaderboard)
        {
            var orderedParticipants = leaderboard.Participants.OrderByDescending((participant) => participant.Points);
            int i = 1;
            foreach (ParticipantModel participant in orderedParticipants)
            {
                participant.Position = i++;
            }
            leaderboard.Participants = new ObservableCollection<ParticipantModel>(orderedParticipants);
            return leaderboard;
        }

        private ParticipantModel AddLoss(ParticipantModel loser, int points)
        {
            loser.Losses++;
            loser.Points -= Math.Max(3, points);
            loser.Points = Math.Max(0, loser.Points);
            return loser;
        }

        private ParticipantModel AddWin(ParticipantModel winner, int points)
        {
            winner.Wins++;
            winner.Points += Math.Max(3, points);
            return winner;
        }

        private LeaderboardModel RecalculatePoints(LeaderboardModel leaderboard, ParticipantModel winner, ParticipantModel loser)
        {
            double pointsDifference = winner.Points - loser.Points;
            double compensationPoints = (int)Math.Round(pointsDifference / 30);
            compensationPoints = Math.Clamp(compensationPoints, -10, 2);
            var resultPoints = (int)(5 - compensationPoints);

            var winnerIndex = leaderboard.Participants.IndexOf(winner);
            leaderboard.Participants[winnerIndex] = AddWin(winner, resultPoints);

            var loserIndex = leaderboard.Participants.IndexOf(loser);
            leaderboard.Participants[loserIndex] = AddLoss(loser, resultPoints);
            return leaderboard;
        }
    }
}
