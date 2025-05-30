using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TasksTracker.Converters
{
    public class HexToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string hex && !string.IsNullOrEmpty(hex))
            {
                try
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
                }
                catch
                {
                    return Brushes.White;
                }
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}