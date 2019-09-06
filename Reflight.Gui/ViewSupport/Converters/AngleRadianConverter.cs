using System;
using Windows.UI.Xaml.Data;
using static System.Math;

namespace Reflight.Gui.ViewSupport.Converters
{
    public class AngleRadianConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var v = System.Convert.ToDouble(value);
            return v * 180D / PI;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var v = System.Convert.ToDouble(value);
            return v * PI / 180D;
        }
    }
}