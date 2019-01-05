using PVPMistico.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PVPMistico.Converters
{
    public class IsAdminToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool isAdmin)
            {
                if (isAdmin)
                    return AppResources.CeaseAdmin;
                else
                    return AppResources.AddAsAdmin;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
