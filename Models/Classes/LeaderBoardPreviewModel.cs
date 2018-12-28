using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Classes
{
    public class LeaderBoardPreviewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ParticipantsModel Participant { get; set; }

        public string LeagueType { get; set; }
    }
}
