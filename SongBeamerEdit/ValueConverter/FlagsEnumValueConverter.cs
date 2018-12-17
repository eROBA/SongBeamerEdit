using System;
using System.Windows.Data;
using System.Globalization;

namespace SongBeamerEdit.FlagsValueConverter
{
    public class FlagsEnumValueConverter :IValueConverter
    {
        private ushort targetValue;

        public FlagsEnumValueConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort mask = (ushort)parameter;
            this.targetValue = (ushort)value;

            return ((mask & this.targetValue) != 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.targetValue ^= (ushort)parameter;
            return Enum.Parse(targetType, this.targetValue.ToString());
        }
    }
}
