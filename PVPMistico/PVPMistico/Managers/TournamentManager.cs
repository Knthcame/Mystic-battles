using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Models.Classes;
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
                    LeagueType = "Great League",
                    Name = "Originals great",
                    Participants = new ObservableCollection<ParticipantsModel>()
                    {
                        new ParticipantsModel()
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
                                    LeagueType = "Great League",
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = "GreatLeague",
                                    ID = 2,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                }
                            }
                        },
                        new ParticipantsModel()
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
                                    LeagueType = "Great League",
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = "GreatLeague",
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
                    LeagueType = "Ultra League",
                    Name = "Originals ultra",
                    Participants = new ObservableCollection<ParticipantsModel>()
                    {
                        new ParticipantsModel()
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
                                    LeagueType = "Ultra League",
                                    ID = 1,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = "Ultra League",
                                    ID = 2,
                                    Loser = "Originals",
                                    Winner = "No originals"
                                }
                            }
                        },
                        new ParticipantsModel()
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
                                    LeagueType = "Ultra League",
                                    ID = 3,
                                    Winner = "Originals",
                                    Loser = "No originals"
                                },
                                new MatchModel()
                                {
                                    League = "Originals",
                                    LeagueType = "Ultra League",
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
