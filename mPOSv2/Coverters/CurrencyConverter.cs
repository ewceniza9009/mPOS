using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace mPOSv2.Converters
{
    internal class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return Decimal.Parse(value.ToString()).ToString("N2", culture);
            return $"{NumberFormatInfo.CurrentInfo.CurrencySymbol}{decimal.Parse(value.ToString()):N2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
}