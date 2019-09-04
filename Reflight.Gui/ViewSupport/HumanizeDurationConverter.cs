using System;
using Windows.UI.Xaml.Data;
using Humanizer;

namespace Reflight.Gui.ViewSupport
{
    public class HumanizeDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is NodaTime.Duration d)
            {
                return d.ToTimeSpan().Humanize();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}