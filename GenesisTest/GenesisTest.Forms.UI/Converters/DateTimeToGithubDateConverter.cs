using System;
using System.Globalization;
using Xamarin.Forms;

namespace GenesisTest.Forms.UI.Converters
{
    public class DateTimeToGithubDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var days = (DateTime.Now - date).Days;

            if (days == 0)
            {
                return $"{date.ToShortDateString()} (Today)";
            }
            else if (days == 1)
            {
                return $"{date.ToShortDateString()} (1 day ago)";
            }
            else
            {
                return $"{date.ToShortDateString()} ({days} days ago)";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
