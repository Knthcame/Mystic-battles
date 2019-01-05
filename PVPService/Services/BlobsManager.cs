using Models.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PVPService.Services
{
    public class BlobsManager
    {
        public List<LeaderboardModel> DeblobLeaderboards(List<LeaderboardModel> list)
        {
            foreach (LeaderboardModel leaderboard in list)
            {
                leaderboard.Participants = DeblobLeaderboard(leaderboard).Participants;
            }
            return list;
        }

        public List<LeaderboardModel> BlobLeaderboards(List<LeaderboardModel> list)
        {
            foreach (LeaderboardModel leaderboard in list)
            {
                leaderboard.Participants = BlobLeaderboard(leaderboard).Participants;
            }
            return list;
        }

        public LeaderboardModel BlobLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard.ParticipantsBlobbed = JsonConvert.SerializeObject(leaderboard.Participants);
            return leaderboard;
        }

        public LeaderboardModel DeblobLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard.Participants = JsonConvert.DeserializeObject<ObservableCollection<ParticipantModel>>(leaderboard.ParticipantsBlobbed);
            return leaderboard;
        }

        public List<MatchModel> DeblobMatches(List<MatchModel> matches)
        {
            var list = new List<MatchModel>();
            foreach(MatchModel match in matches)
            {
                list.Add(DeblobMatch(match));
            }
            return list;
        }

        public List<MatchModel> BlobMatches(List<MatchModel> matches)
        {
            var list = new List<MatchModel>();
            foreach (MatchModel match in matches)
            {
                list.Add(BlobMatch(match));
            }
            return list;
        }

        public MatchModel BlobMatch(MatchModel match)
        {
            match.WinnerBlob = JsonConvert.SerializeObject(match.Winner);
            match.LoserBlob = JsonConvert.SerializeObject(match.Loser);
            return match;
        }

        public MatchModel DeblobMatch(MatchModel match)
        {
            match.Winner = JsonConvert.DeserializeObject<TrainerModel>(match.WinnerBlob);
            match.Loser = JsonConvert.DeserializeObject<TrainerModel>(match.LoserBlob);
            return match;
        }
    }
}
