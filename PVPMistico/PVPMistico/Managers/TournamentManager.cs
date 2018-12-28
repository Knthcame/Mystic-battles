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

        public TournamentManager(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public LeaderboardModel CreateTournament()
        {
            throw new System.NotImplementedException();
        }

        public LeaderboardModel GetLeaderboard(int id)
        {
            return GetLeaderboards().FirstOrDefault((boards) => boards.ID == id);
        }

        public List<LeaderboardModel> GetLeaderboards()
        {
            return new List<LeaderboardModel>()
            {
                new LeaderboardModel()
                {
                    ID = 1,
                    LeagueType = LeagueTypes.GreatLeague,
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
                                    LeagueType = LeagueTypes.GreatLeague,
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypes.GreatLeague,
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
                            Username = "No Originals",
                            Wins = 0,
                            Position = 2,
                            Points = 0,
                            Matches = new ObservableCollection<MatchModel>()
                            {
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypes.GreatLeague,
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypes.GreatLeague,
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
                    LeagueType = LeagueTypes.UltraLeague,
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
                                    LeagueType = LeagueTypes.UltraLeague,
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypes.UltraLeague,
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
                                    LeagueType = LeagueTypes.UltraLeague,
                                    ID = 3,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = LeagueTypes.UltraLeague,
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

        public IEnumerable<LeaderboardModel> GetMyLeaderboards(string username)
        {
            return GetLeaderboards().Where((boards) => boards.Participants.Any((participant) => participant.Username == username));
        }
    }
}
