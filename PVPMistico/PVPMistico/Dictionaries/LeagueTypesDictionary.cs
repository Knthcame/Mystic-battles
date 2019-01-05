using Models.Enums;
using PVPMistico.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVPMistico.Dictionaries
{
    public static class LeagueTypesDictionary
    {
        private static Dictionary<LeagueTypesEnum, string> _dictionary = new Dictionary<LeagueTypesEnum, string>()
        {
            {LeagueTypesEnum.GreatLeague, AppResources.GreatLeague },
            {LeagueTypesEnum.UltraLeague, AppResources.UltraLeague },
            {LeagueTypesEnum.MasterLeague, AppResources.MasterLeague }
        };

        public static bool GetLeagueTypeString(LeagueTypesEnum leagueType, out string str)
        {
            return _dictionary.TryGetValue(leagueType, out str);
        }
    }
}
