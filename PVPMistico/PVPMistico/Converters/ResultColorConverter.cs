using PVPMistico.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class ResultColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (MatchResultEnum)value;
                switch (result)
                {
                    case MatchResultEnum.Win:
                        return Color.Green;

                    case MatchResultEnum.Defeat:
                        return Color.Red;

                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
