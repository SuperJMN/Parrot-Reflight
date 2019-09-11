using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Reflight.Gui.ViewModels;

namespace Reflight.Gui
{
    public class SectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is NavigationViewItemInvokedEventArgs args)
            {
                if (args.InvokedItem is Section s)
                {
                    return s;
                }

                if (args.IsSettingsInvoked)
                {
                    return new Section("Settings");
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}