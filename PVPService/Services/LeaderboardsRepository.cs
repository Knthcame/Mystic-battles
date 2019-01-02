using Models.Classes;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PVPService.Services
{
    public static class LeaderboardsRepository
    {
        private static List<LeaderboardModel> _leaderboards = LeaderboardMock();

        public static List<LeaderboardModel> GetLeaderboards() => _leaderboards;

        public static List<LeaderboardModel> GetUserLeaderBoards(string username) => 
            new List<LeaderboardModel>(_leaderboards.Where((boards) => boards.Trainers.Any((participant) => participant.Username == username)));

        public static LeaderboardModel GetLeaderboard(int id) =>
            _leaderboards.FirstOrDefault((board) => board.ID == id);

        public static bool InputMatch(int id, MatchModel match)
        {
            if (match == null)
                return false;

            var leaderboard = _leaderboards.FirstOrDefault((board) => board.ID == id);
            if (leaderboard == null)
                return false;

            var winner = leaderboard.Trainers.FirstOrDefault((participant) => participant.Username == match.Winner);
            var loser = leaderboard.Trainers.FirstOrDefault((participant) => participant.Username == match.Loser);

            if (winner == null || loser == null)
                return false;

            match.DateTime = DateTime.Now;

            AddWin(winner, match);
            AddLoss(loser, match);
            RecalculatePositions(leaderboard);
            return true;
        }

        public static CreateLeaderboardResponseCode AddLeaderboard(LeaderboardModel leaderboard)
        {
            if (leaderboard == null || leaderboard.Trainers == null)
                return CreateLeaderboardResponseCode.UnknownError;

            if (_leaderboards.Any(board => board.Name == leaderboard.Name))
                return CreateLeaderboardResponseCode.NameAlreadyUsed;

            leaderboard.ID = GenerateNewID();
            _leaderboards.Add(leaderboard);
            return CreateLeaderboardResponseCode.CreatedSuccessfully;
        }

        public static AddTrainerResponseCode AddTrainer(int leaderboardId, TrainerModel trainer)
        {
            if (trainer == null)
                return AddTrainerResponseCode.UnknownError;

            var leaderboard = _leaderboards.FirstOrDefault((board) => board.ID == leaderboardId);
            if (leaderboard == null)
                return AddTrainerResponseCode.UnknownError;

            if (leaderboard.Trainers.Any(participant => participant.Username == trainer.Username))
                return AddTrainerResponseCode.TrainerAlreadyParticipates;

            trainer.Position = leaderboard.Trainers.Count + 1;

            leaderboard.Trainers.Add(trainer);
            return AddTrainerResponseCode.TrainerAddedSuccesfully;
        }

        private static int GenerateNewID()
        {
            int newId;
            do
            {
                newId = new Random().Next();
            }
            while (_leaderboards.Any((board => board.ID == newId)));

            return newId;
        }

        private static void RecalculatePositions(LeaderboardModel leaderboard)
        {
            var orderedParticipants = leaderboard.Trainers.OrderByDescending((participant) => participant.Points);
            int i = 1;
            foreach (TrainerModel participant in orderedParticipants)
            {
                participant.Position = i++;
            }
        }

        private static void AddLoss(TrainerModel loser, MatchModel match)
        {
            loser.Losses++;
            loser.Matches.Add(match);
        }

        private static void AddWin(TrainerModel winner, MatchModel match)
        {
            winner.Wins++;
            winner.Points += 3;
            winner.Matches.Add(match);
        }

        private static List<LeaderboardModel> LeaderboardMock()
        {
            return new List<LeaderboardModel>()
            {
                new LeaderboardModel()
                {
                    ID = 1,
                    LeagueType = LeagueTypesEnum.GreatLeague,
                    Name = "Originals great",
                    Trainers = new ObservableCollection<TrainerModel>()
                    {
                        new TrainerModel()
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
                        new TrainerModel()
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
                    Trainers = new ObservableCollection<TrainerModel>()
                    {
                        new TrainerModel()
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
                        new TrainerModel()
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
