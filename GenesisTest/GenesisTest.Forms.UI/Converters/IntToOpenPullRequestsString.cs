using System;
using System.Globalization;
using Xamarin.Forms;

namespace GenesisTest.Forms.UI.Converters
{
    public class IntToOpenPullRequestsString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int)value;

            return $"{count} opened";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
