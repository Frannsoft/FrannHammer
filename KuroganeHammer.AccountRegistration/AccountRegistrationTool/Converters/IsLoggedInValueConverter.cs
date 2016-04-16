using System;
using System.Globalization;
using System.Windows.Data;

namespace AccountRegistrationTool.Converters
{
    public class IsLoggedInValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isLoggedIn = (bool)value;

            if (isLoggedIn)
            {
                return "Logged In!";
            }
            else
            {
                return "Not Logged in.";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
