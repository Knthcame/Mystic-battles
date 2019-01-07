using Models.Classes;
using PVPMistico.Enums;
using PVPMistico.Managers.Interfaces;
using PVPMistico.Models;
using System.Collections.Generic;

namespace PVPMistico.Extensions
{
    public static class TrainerMatchModelExtensions
    {
        public static List<TrainerMatchModel> ToTrainerMatchModel(this List<MatchModel> matches, string trainerUsername)
        {
            if (matches == null)
                return null;

            var list = new List<TrainerMatchModel>();

            foreach(MatchModel match in matches)
            {
                list.Add(match.ToTrainerMatchModel(trainerUsername));
            }
            return list;
        }

        public static TrainerMatchModel ToTrainerMatchModel(this MatchModel match, string trainerUsername)
        {
            var trainerMatch = new TrainerMatchModel();

            if (match.Winner.Username == trainerUsername)
            {
                trainerMatch.Result = MatchResultEnum.Win;
                trainerMatch.Trainer = match.Winner;
                trainerMatch.Opponent = match.Loser;
            }
            else if (match.Loser.Username == trainerUsername)
            {
                trainerMatch.Result = MatchResultEnum.Defeat;
                trainerMatch.Trainer = match.Loser;
                trainerMatch.Opponent = match.Winner;
            }
            else
                return null;

            trainerMatch.DateTime = match.DateTime;
            trainerMatch.ID = match.ID;
            trainerMatch.Points = match.Points;

            return trainerMatch;
        }
    }
}
