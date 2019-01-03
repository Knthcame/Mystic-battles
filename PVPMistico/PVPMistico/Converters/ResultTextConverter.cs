using PVPMistico.Enums;
using PVPMistico.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class ResultTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (MatchResultEnum)value;
                switch (result)
                {
                    case MatchResultEnum.Win:
                        return AppResources.Win;

                    case MatchResultEnum.Defeat:
                        return AppResources.Defeat;

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
