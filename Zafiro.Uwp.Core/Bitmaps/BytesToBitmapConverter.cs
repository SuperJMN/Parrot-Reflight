using System;
using Windows.UI.Xaml.Data;

namespace Zafiro.Uwp.Core.Bitmaps
{
    public class BytesToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is byte[] bytes ? BitmapConversion.FromBytes(bytes) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}