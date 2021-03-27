using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace mPOSv2.Converters
{
    internal class SelectedIndexChangedArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as PropertyChangedEventArgs;

            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}