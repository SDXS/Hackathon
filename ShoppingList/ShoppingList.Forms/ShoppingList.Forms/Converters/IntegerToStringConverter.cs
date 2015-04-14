namespace ShoppingList.Forms.Converters
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public class IntegerToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number;
            return int.TryParse(value as string ?? string.Empty, out number) ? number : 0;
        }
    }
}
