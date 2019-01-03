using Models.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PVPService.Services
{
    public class MatchesRepository
    {
        private Database _database = new Database();

        private readonly BlobsManager _blobsManager = new BlobsManager();

        public List<MatchModel> GetMatches()
        {
            var matches =_database.GetMatches();
            matches = SetMatchesLeaderboards(matches);
            matches = _blobsManager.DeblobMatches(matches);
            return matches;
        }

        public List<MatchModel> GetLeagueMatches(int leagueId)
        {
            var matches = _database.GetMatches().Where(match => match.LeagueID == leagueId).ToList();
            matches = _blobsManager.DeblobMatches(matches);
            matches = SetMatchesLeaderboards(matches);
            return matches;
        }

        public List<MatchModel> GetTrainerMatches(string username)
        {
            var matches = _database.GetMatches().ToList();
            matches = _blobsManager.DeblobMatches(matches);
            matches = FilterByUsername(matches, username).ToList();
            matches = SetMatchesLeaderboards(matches);
            return matches;
        }

        public List<MatchModel> GetTrainerLeagueMatches(string username, int leagueId)
        {
            var matches = _database.GetMatches().ToList();
            matches = FilterByLeagueId(matches, leagueId).ToList();
            matches = _blobsManager.DeblobMatches(matches);
            matches = FilterByUsername(matches, username).ToList();
            matches = SetMatchesLeaderboards(matches);
            return matches;
        }

        private IEnumerable<MatchModel> FilterByLeagueId(List<MatchModel> matches, int leagueId)
        {
            return matches.Where(match => match.LeagueID == leagueId);
        }

        private IEnumerable<MatchModel> FilterByUsername(List<MatchModel> matches, string username)
        {
            return matches.Where(match => match.Winner.Username == username || match.Loser.Username == username);
        }
        

        private List<MatchModel> SetMatchesLeaderboards(List<MatchModel> matches)
        {
            var list = new List<MatchModel>();
            foreach (MatchModel match in matches)
            {
                list.Add(SetMatchLeaderboard(match));
            }
            return list;
        }

        private MatchModel SetMatchLeaderboard(MatchModel match)
        {
            var leaderboard = _database.GetLeaderboards().Find(board => board.ID == match.LeagueID);

            match.LeagueName = leaderboard.Name;
            match.LeagueType = leaderboard.LeagueType;

            return match;

        }
    }
}
