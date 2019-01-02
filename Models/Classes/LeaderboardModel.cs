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

        public ObservableCollection<ParticipantModel> Participant { get; set; }

        public LeagueTypesEnum LeagueType { get; set; }
    }
}
