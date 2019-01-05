using Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace Models.Classes
{
    [Table(nameof(LeaderboardModel))]
    public class LeaderboardModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string ParticipantsBlobbed { get; set; }

        [Ignore]
        public ObservableCollection<ParticipantModel> Participants { get; set; }

        public LeagueTypesEnum LeagueType { get; set; }
    }
}
