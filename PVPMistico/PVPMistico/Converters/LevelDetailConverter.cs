using PVPMistico.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class LevelDetailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AppResources.Level + ": " + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
