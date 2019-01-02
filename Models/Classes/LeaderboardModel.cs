using Models.Enums;
using System.Collections.ObjectModel;

namespace Models.Classes
{
    public class LeaderboardModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ObservableCollection<TrainerModel> Trainers { get; set; }

        public LeagueTypesEnum LeagueType { get; set; }
    }
}
