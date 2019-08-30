using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace Reflight.Universal.Core.Bitmaps
{
    public class StorageFileToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is StorageFile file)
            {
                var bytes = file.GetThumbnailBytes().Result;
                return BitmapConversion.FromBytes(bytes);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}