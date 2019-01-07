using Models.Enums;
using SQLite;
using System;

namespace Models.Classes
{
    public class MatchModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        public string WinnerBlob { get; set; }

        [Ignore]
        public TrainerModel Winner { get; set; }

        public string LoserBlob { get; set; }

        [Ignore]
        public TrainerModel Loser { get; set; }

        public int LeagueID { get; set; }

        [Ignore]
        public string LeagueName { get; set; }

        [Ignore]
        public LeagueTypesEnum LeagueType { get; set; }

        public DateTime DateTime { get; set; }
        
        public int Points { get; set; } = 5;
    }
}
