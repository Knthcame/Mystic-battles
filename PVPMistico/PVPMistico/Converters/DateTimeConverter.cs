using System;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var dateTime = (DateTime) value;
                dateTime = dateTime.ToLocalTime();
                var convertedDate = dateTime.ToString("dd MMMM yyyy HH:mm");
                return convertedDate;
            }
            catch(Exception e)
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
