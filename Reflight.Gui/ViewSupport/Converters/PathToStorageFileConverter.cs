using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace Reflight.Gui
{
    public class PathToStorageFileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string path)
            {
                var st = StorageFile.GetFileFromPathAsync(path).GetAwaiter().GetResult();
                return st;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}