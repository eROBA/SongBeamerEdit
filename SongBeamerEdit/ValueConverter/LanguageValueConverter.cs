using System;
using System.Globalization;
using System.Windows.Data;
using SongBeamerEdit.ViewModel;

namespace SongBeamerEdit.FlagsValueConverter
{
    public class LanguageValueConverter : IValueConverter
    {
        private Language target;

        public LanguageValueConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Language mask = (Language)parameter;
            this.target = (Language)value;
            return ((mask & this.target) != 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.target ^= (Language)parameter;
            return this.target;
        }
    }
}
