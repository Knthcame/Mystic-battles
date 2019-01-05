using PVPMistico.Enums;
using PVPMistico.Resources;
using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class TrainerResultImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (MatchResultEnum) value;
                switch (result)
                {
                    case MatchResultEnum.Win:
                        return AppImages.WinGesture;

                    case MatchResultEnum.Defeat:
                        return AppImages.DefeatGesture;

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
