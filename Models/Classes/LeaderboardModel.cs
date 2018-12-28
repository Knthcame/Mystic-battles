using System.Collections.ObjectModel;

namespace Models.Classes
{
    public class LeaderboardModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ObservableCollection<ParticipantsModel> Participants { get; set; }

        public string LeagueType { get; set; }
    }
}
