using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Reflight.Core;

namespace Reflight.Gui.ViewSupport.Converters
{
    public class DroneModelConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DroneModel dm)
            {
                if (dm.Name == "Anafi")
                {
                    return AnafiValue;
                }

                if (dm.Name == "Disco")
                {
                    return DiscoValue;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public static readonly DependencyProperty AnafiValueProperty = DependencyProperty.Register(
            "AnafiValue", typeof(Uri), typeof(DroneModelConverter), new PropertyMetadata(default(Uri)));

        public Uri AnafiValue
        {
            get { return (Uri) GetValue(AnafiValueProperty); }
            set { SetValue(AnafiValueProperty, value); }
        }

        public static readonly DependencyProperty DiscoValueProperty = DependencyProperty.Register(
            "DiscoValue", typeof(Uri), typeof(DroneModelConverter), new PropertyMetadata(default(Uri)));

        public Uri DiscoValue
        {
            get { return (Uri) GetValue(DiscoValueProperty); }
            set { SetValue(DiscoValueProperty, value); }
        }
    }
}