using Models.Enums;

namespace PVPMistico.Models
{
    public class LeagueTypePickerItemModel
    {
        public LeagueTypesEnum LeagueTypesEnum { get; set; }

        public string LeagueTypeName { get; set; }

        public LeagueTypePickerItemModel(LeagueTypesEnum leagueTypesEnum, string leagueTypeName)
        {
            LeagueTypesEnum = leagueTypesEnum;
            LeagueTypeName = leagueTypeName;
        }
    }
}
