using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace SongBeamerEdit.FlagsValueConverter
{
    public class FlagsEnumToVisibilityConverter : IValueConverter
    {
        private ushort targetValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort mask = (ushort)parameter;
            this.targetValue = (ushort)value;
            var test = ((mask & this.targetValue) != 0) ? Visibility.Visible : Visibility.Hidden;
            return test;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
