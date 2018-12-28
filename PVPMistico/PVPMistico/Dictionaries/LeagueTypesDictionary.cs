using Models.Enums;
using PVPMistico.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVPMistico.Dictionaries
{
    public static class LeagueTypesDictionary
    {
        private static Dictionary<LeagueTypes, string> _dictionary = new Dictionary<LeagueTypes, string>()
        {
            {LeagueTypes.GreatLeague, AppResources.GreatLeague },
            {LeagueTypes.UltraLeague, AppResources.UltraLeague },
            {LeagueTypes.MasterLeague, AppResources.MasterLeague }
        };

        public static bool GetLeagueTypeString(LeagueTypes leagueType, out string str)
        {
            return _dictionary.TryGetValue(leagueType, out str);
        }
    }
}
