using System.Threading.Tasks;
using NodaTime;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{ 
    public class Video
    {
        public IFile Source { get; set; }
        public Interval? RecordedInterval { get; set; }

        public byte[] Thumbnail { get; private set; }

        public double Width { get; private set; }

        public double Height{ get; private set; }

        public static async Task<Video> Load(IFile file)
        {
            return new Video
            {
                RecordedInterval = await file.GetInterval(),
                Source = file,
                Thumbnail = await file.GetThumbnail(),
                Width = await file.GetFrameWidth(),
                Height = await file.GetFrameHeight(),
            };
        }
    }
}