using Models.Enums;
using PVPMistico.Dictionaries;
using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class LeagueTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var leagueType = (LeagueTypes) value;
                if (LeagueTypesDictionary.GetLeagueTypeString(leagueType, out string str))
                    return str;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
