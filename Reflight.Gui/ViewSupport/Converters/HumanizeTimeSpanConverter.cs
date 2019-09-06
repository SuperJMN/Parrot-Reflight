using System;
using Windows.UI.Xaml.Data;
using Humanizer;

namespace Reflight.Gui.ViewSupport
{
    public class HumanizeTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan ts)
            {
                return ts.Humanize();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}