using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Models.Classes;
using Models.Enums;
using PVPMistico.Managers.Interfaces;

namespace PVPMistico.Managers
{
    public class TournamentManager : ITournamentManager
    {
        private readonly IHttpManager _httpManager;

        private static List<LeaderboardModel> leaderboards = LeaderboardMock();

        public TournamentManager(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public bool CreateTournament(string name, LeagueTypesEnum leagueType, ParticipantModel creator)
        {
            if (creator == null)
                return false;

            creator.IsAdmin = true;
            leaderboards.Add(new LeaderboardModel()
            {
                ID = leaderboards.Count + 1,
                LeagueType = leagueType,
                Name = name,
                Participants = new ObservableCollection<ParticipantModel>()
                {
                    creator
                }
            });
            return true;
        }

        public LeaderboardModel GetLeaderboard(int id)
        {
            return GetLeaderboards().FirstOrDefault((boards) => boards.ID == id);
        }

        public List<LeaderboardModel> GetLeaderboards()
        {
            return leaderboards;
        }

        public IEnumerable<LeaderboardModel> GetMyLeaderboards(string username)
        {
            return GetLeaderboards().Where((boards) => boards.Participants.Any((participant) => participant.Username == username));
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

        private static List<LeaderboardModel> LeaderboardMock()
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
