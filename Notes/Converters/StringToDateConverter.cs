using System;
using System.Globalization;
using System.Windows.Data;

namespace TasksTracker.Converters
{
    public class StringToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateStr)
            {
                if (dateStr.Equals("Сегодня", StringComparison.OrdinalIgnoreCase))
                {
                    return DateTime.Today;
                }
                if (DateTime.TryParse(dateStr, out DateTime date))
                {
                    return date;
                }
            }
            return DateTime.Today;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("dd.MM.yyyy");
            }
            return "Сегодня";
        }
    }
}