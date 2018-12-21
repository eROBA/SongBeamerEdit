using System;
using System.Windows.Data;
using System.Globalization;

namespace SongBeamerEdit.FlagsValueConverter
{
    public class FlagsEnumValueConverter :IValueConverter
    {
        private int targetValue;

        public FlagsEnumValueConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int mask = (int)parameter;
            this.targetValue = (int)value;

            return ((mask & this.targetValue) != 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.targetValue ^= (int)parameter;
            return Enum.Parse(targetType, this.targetValue.ToString());
        }
    }
}
