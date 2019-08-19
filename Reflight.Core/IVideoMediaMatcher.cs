using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Extensions;

namespace Reflight.Core
{
    public interface IVideoMediaMatcher
    {
        IObservable<string> Lookup(Interval interval, IList<string> folders);
    }

    public class MediaStore : IVideoMediaMatcher
    {
        private readonly IFileSystem fileSystem;

        public MediaStore(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public IObservable<string> Lookup(Interval interval, IList<string> folders)
        {
            var inside = folders.ToObservable()
                .SelectMany(s => fileSystem.GetFilesFromFolder(s))
                .SelectMany(async fileName => new { Interval = await GetInterval(fileName), File = fileName })
                .Where(x => interval.Contains(x.Interval))
                .Select(x => x.File);

            return inside;
        }

        private async Task<Interval> GetInterval(string fileName)
        {
            var dateEncoded = await fileSystem.GetMetadata<DateTimeOffset?>(fileName, fileSystem.Metadata.DateEncoded);
            var encodedDuration = await fileSystem.GetMetadata<ulong?>(fileName, fileSystem.Metadata.Duration);

            if (dateEncoded.HasValue && encodedDuration.HasValue)
            {
                var duration = ToTimeSpan(encodedDuration.Value);
                var end = dateEncoded.Value.Add(duration);
                return new Interval(dateEncoded.Value.ToInstant(), end.ToInstant());
            }

            return new Interval(Instant.MinValue, Instant.MinValue);
        }

        private static TimeSpan ToTimeSpan(ulong encodedDurationValue)
        {
            return TimeSpan.FromMilliseconds(0.0001 * encodedDurationValue);
        }
    }

    public static class IntervalMixin
    {
        public static bool Contains(this Interval subject, Interval container)
        {
            return subject.Contains(container.Start) && subject.Contains(container.End);
        }
    }
}