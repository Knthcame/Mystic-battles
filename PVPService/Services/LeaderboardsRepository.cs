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

        public List<LeaderboardModel> GetLeaderboards() => _database.GetLeaderboards();

        public List<LeaderboardModel> GetUserLeaderBoards(string username) => 
            new List<LeaderboardModel>(_database.GetLeaderboards().Where((boards) => boards.Participants.Any((participant) => participant.Username == username)));

        public LeaderboardModel GetLeaderboard(int id) =>
            _database.GetLeaderboards().FirstOrDefault((board) => board.ID == id);

        public bool InputMatch(int id, MatchModel match)
        {
            if (match == null)
                return false;

            var leaderboard = _database.GetLeaderboards().FirstOrDefault((board) => board.ID == id);
            if (leaderboard == null)
                return false;

            var winner = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == match.Winner);
            var loser = leaderboard.Participants.FirstOrDefault((participant) => participant.Username == match.Loser);

            if (winner == null || loser == null)
                return false;

            match.DateTime = DateTime.Now;

            AddWin(winner, match);
            AddLoss(loser, match);
            RecalculatePositions(leaderboard);
            return true;
        }

        public CreateLeaderboardResponseCode AddLeaderboard(LeaderboardModel leaderboard)
        {
            if (leaderboard == null || leaderboard.Participants == null)
                return CreateLeaderboardResponseCode.UnknownError;

            if (_database.GetLeaderboards().Any(board => board.Name == leaderboard.Name))
                return CreateLeaderboardResponseCode.NameAlreadyUsed;

            //leaderboard.ID = GenerateNewID();
            if (_database.AddLeaderboard(leaderboard))
                return CreateLeaderboardResponseCode.CreatedSuccessfully;
            else
                return CreateLeaderboardResponseCode.UnknownError;
        }

        public AddTrainerResponseCode AddTrainer(int leaderboardId, ParticipantModel trainer)
        {
            if (trainer == null)
                return AddTrainerResponseCode.UnknownError;

            var leaderboard = _database.GetLeaderboards().FirstOrDefault((board) => board.ID == leaderboardId);
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

        private void AddLoss(ParticipantModel loser, MatchModel match)
        {
            loser.Losses++;
            loser.Matches.Add(match);
        }

        private void AddWin(ParticipantModel winner, MatchModel match)
        {
            winner.Wins++;
            winner.Points += 3;
            winner.Matches.Add(match);
        }

        private List<LeaderboardModel> LeaderboardMock()
        {
            return new List<LeaderboardModel>()
            {
                new LeaderboardModel()
                {
                    ID = 1,
                    LeagueType = LeagueTypesEnum.GreatLeague,
                    Name = "Originals great",
                    Participants = new ObservableCollection<ParticipantModel>()
                    {
                        new ParticipantModel()
                        {
                            Level = 40,
                            Losses = 0,
                            Username = "Originals",
                            Wins = 2,
                            IsAdmin = true,
                            Position = 1,
                            Points = 6,
                            Matches = new ObservableCollection<MatchModel>()
                            {
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.GreatLeague,
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.GreatLeague,
                                    ID = 2,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                }
                            }
                        },
                        new ParticipantModel()
                        {
                            Level = 40,
                            Losses = 2,
                            Username = "No originals",
                            Wins = 0,
                            Position = 2,
                            Points = 0,
                            Matches = new ObservableCollection<MatchModel>()
                            {
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.GreatLeague,
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.GreatLeague,
                                    ID = 2,
                                    Loser = "Originals",
                                    Winner = "No originals"
                                }
                            }
                        }
                    }
                },
                new LeaderboardModel()
                {
                    ID = 2,
                    LeagueType = LeagueTypesEnum.UltraLeague,
                    Name = "Originals ultra",
                    Participants = new ObservableCollection<ParticipantModel>()
                    {
                        new ParticipantModel()
                        {
                            Level = 40,
                            Losses = 0,
                            Username = "Originals",
                            Wins = 2,
                            Position = 1,
                            Points = 6,
                            Matches = new ObservableCollection<MatchModel>()
                            {
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.UltraLeague,
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.UltraLeague,
                                    ID = 2,
                                    Loser = "Originals",
                                    Winner = "No originals"
                                }
                            }
                        },
                        new ParticipantModel()
                        {
                            Level = 40,
                            Losses = 2,
                            Username = "No originals",
                            Wins = 0,
                            Position = 2,
                            Points = 0,
                            Matches = new ObservableCollection<MatchModel>()
                            {
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.UltraLeague,
                                    ID = 3,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypesEnum.UltraLeague,
                                    ID = 4,
                                    Loser = "Originals",
                                    Winner = "No originals"
                                }
                            }
                        }
                    }
                }
            };
        }

    }
}
