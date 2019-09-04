using System;
using System.Reactive.Linq;
using NodaTime;

namespace Reflight.Core
{
    public class MediaStore : IMediaStore
    {
        private readonly IFileSystem fileSystem;

        public MediaStore(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public IObservable<IFile> RecordingsBetween(Interval interval)
        {
            var inside = fileSystem.GetAllFiles()
                .SelectMany(async file => new { Interval = await file.GetInterval(), File = file })
                .Where(x => x.Interval != null && interval.Contains(x.Interval.Value))
                .Select(x => x.File);

            return inside;
        }
    }
}