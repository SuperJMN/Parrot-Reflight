using System;
using System.Reactive.Linq;
using Windows.Storage;
using NodaTime;
using Reflight.Core;

namespace Reflight.Universal.Core.Filesystem
{
    public class ContentMatcher
    {
        public IObservable<StorageFile> Match(Interval interval)
        {
            var store = new MediaStore(new UwpFileSystem());
            var matches = store
                .RecordingsBetween(interval)
                .SelectMany(StorageFile.GetFileFromPathAsync);

            return matches;
        }
    }
}