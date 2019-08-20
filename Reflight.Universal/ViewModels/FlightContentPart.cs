using System;
using System.IO;

namespace Reflight.Universal.ViewModels
{
    public abstract class FlightContentPart
    {
        public byte[] Thumbnail { get; private set; }

        public static FlightContentPart FromFile(string path)
        {
            if (Path.GetExtension(path).Equals(".mp4", StringComparison.InvariantCultureIgnoreCase))
            {
                return new VideoContentPart(path);
            }

            throw new InvalidOperationException();
        }
    }

    public class VideoContentPart : FlightContentPart
    {
        private readonly string path;

        public VideoContentPart(string path)
        {
            this.path = path;
        }
    }
}