using Models.Classes;
using PVPMistico.Enums;
using System;

namespace PVPMistico.Models
{
    public class TrainerMatchModel
    {
        public int ID { get; set; }

        public MatchResultEnum Result { get; set; }

        public TrainerModel Trainer { get; set; }

        public TrainerModel Opponent { get; set; }

        public DateTime DateTime { get; set; }
    }
}
