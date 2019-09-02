using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Zafiro.Uwp.Core.Bitmaps
{
    public static class BitmapConversion
    {
        public static BitmapImage FromBytes(byte[] bytes)
        {
            using (var ms = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(bytes);
                    writer.StoreAsync().GetResults();
                }
                var image = new BitmapImage();
                image.SetSource(ms);
                return image;
            }
        }
    }
}