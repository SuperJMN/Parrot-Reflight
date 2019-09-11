using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Reflight.Gui.ViewModels;

namespace Reflight.Gui.ViewSupport.Converters
{
    public class SectionIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Section s)
            {
                Symbol symbol = Symbol.Clear;

                switch (s.Name)
                {
                    case "Gallery":
                        symbol = Symbol.BrowsePhotos;
                        break;
                    case "About":
                        symbol = Symbol.ContactInfo;
                        break;
                    case "Donation":
                        symbol = Symbol.Emoji2;
                        break;
                }

                return new SymbolIcon(symbol);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}