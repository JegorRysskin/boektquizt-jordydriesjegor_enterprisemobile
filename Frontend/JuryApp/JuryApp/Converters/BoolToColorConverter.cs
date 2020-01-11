using System;
using Windows.UI.Xaml.Data;

namespace JuryApp.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return "#89C543";

            return "#EC008C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value.ToString() == "#89C543";
        }
    }
}
