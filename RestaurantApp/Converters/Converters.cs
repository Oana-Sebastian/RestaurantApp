using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Globalization;
using System.Windows.Media;

namespace RestaurantApp.Converters
{

        public class BooleanToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                bool bValue = false;
                if (value is bool)
                {
                    bValue = (bool)value;
                }
                return bValue ? Visibility.Visible : Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is Visibility)
                {
                    return (Visibility)value == Visibility.Visible;
                }
                return false;
            }
        }

        public class InverseBooleanToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                bool bValue = false;
                if (value is bool)
                {
                    bValue = (bool)value;
                }
                return bValue ? Visibility.Collapsed : Visibility.Visible;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is Visibility)
                {
                    return (Visibility)value != Visibility.Visible;
                }
                return true;
            }
        }

    public class OrderStatusToBrushConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Gray);

            string status = value.ToString().ToLower();

            return status switch
            {
                "registered" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2196F3")),  // Blue
                "preparing" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF9800")),   // Orange
                "out for delivery" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9C27B0")), // Purple
                "delivered" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50")),   // Green
                "cancelled" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF44336")),   // Red
                _ => new SolidColorBrush(Colors.Gray)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
