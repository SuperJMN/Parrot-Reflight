using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Humanizer;

namespace Reflight.Gui
{
    public class HumanizeDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset d)
            {
                return d.Humanize();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}