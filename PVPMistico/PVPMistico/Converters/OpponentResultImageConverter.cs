using PVPMistico.Enums;
using PVPMistico.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class OpponentResultImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (MatchResultEnum)value;
                switch (result)
                {
                    case MatchResultEnum.Win:
                        return AppImages.DefeatGesture;

                    case MatchResultEnum.Defeat:
                        return AppImages.WinGesture;

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
