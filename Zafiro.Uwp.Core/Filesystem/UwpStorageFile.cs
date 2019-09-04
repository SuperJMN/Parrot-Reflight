using System;
using System.Threading.Tasks;
using Windows.Storage;
using NodaTime;
using NodaTime.Extensions;
using Reflight.Core;

namespace Zafiro.Uwp.Core.Filesystem
{
    public class UwpStorageFile : IFile
    {
        private readonly StorageFile storageFile;

        public UwpStorageFile(StorageFile storageFile)
        {
            this.storageFile = storageFile;
        }

        public string Path => storageFile.Path;

        public Task<byte[]> GetThumbnail()
        {
            return storageFile.GetThumbnailBytes();
        }

        public async Task<Instant?> GetStart()
        {
            var dateEncoded = await GetProperty<DateTimeOffset?>(DateEncoded);
            return dateEncoded?.ToInstant();
        }

        public async Task<Duration?> GetDuration()
        {
            var encodedDuration = await storageFile.GetProperty<ulong?>(Duration);
            if (encodedDuration.HasValue)
            {
                return TimeSpan.FromMilliseconds(0.0001 * encodedDuration.Value).ToDuration();
            }

            return null;
        }

        public async Task<Interval?> GetInterval()
        {
            var duration = await GetDuration();
            var start = await GetStart();
            if (duration.HasValue && start.HasValue)
            {
                return new Interval(start.Value, start.Value.Plus(duration.Value));
            }

            return null;
        }

        public Task<T> GetProperty<T>(string name)
        {
            return storageFile.GetProperty<T>(name);
        }

        public string DateEncoded => "System.Media.DateEncoded";
        public string Duration => "System.Media.Duration";
    }
}