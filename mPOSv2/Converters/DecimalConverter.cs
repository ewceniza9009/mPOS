using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace mPOSv2.Converters
{
    internal class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{decimal.Parse(value.ToString()):N2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            //if (valueFromString.Length <= 0)
            //    return 0m;

            //long valueLong
            //if (!long.TryParse(valueFromString, out valueLong))
            //    return 0m;

            //if (valueLong <= 0)
            //    return 0m;

            //return valueLong / 100m;
            var result = 0m;

            if (value is String && value.ToString().Contains(".."))
            {
                value = value.ToString().Replace("..", ".");
            }

            var txtValue = value.ToString();
            char decSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
            String[] txtValues = txtValue.Split(decSeparator);
            int decCount = txtValues.Length - 1;
            if (decCount > 0)
            {
                String integerPart = txtValues[0];
                String decimalPart = txtValues[decCount];
                if (decimalPart.Length > 2)
                {
                    decimalPart = decimalPart.Substring(0, 2);
                }
                value = integerPart + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + decimalPart;
            }

            if (Decimal.TryParse(value.ToString(), out result))
            {
                result = Math.Round(result, 2);
                result = result > 0 ? result : 0;
            }

            var finalResult = string.Format("{0:N2}", result);

            return finalResult;
        }
    }
}