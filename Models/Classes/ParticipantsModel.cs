using System.Collections.ObjectModel;

namespace Models.Classes
{
    public class ParticipantsModel
    {
        public string Username { get; set; }

        public int Level { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }

        public ObservableCollection<MatchModel> Matches { get; set; }
    }
}
