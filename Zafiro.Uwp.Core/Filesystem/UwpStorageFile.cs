using System;
using System.Linq;
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
            
            if (dateEncoded.HasValue)
            {
                var originalDate = dateEncoded.Value;
                var isDisco = await GetIsDisco(storageFile);
                var date = GetCorrectDate(originalDate, isDisco);
                return date.ToInstant();
            }

            return null;
        }

        private async Task<bool> GetIsDisco(StorageFile file)
        {
            var author = await file.GetProperty<string[]>("System.Author");
            var isDisco = author != null && author.Any(x => x.Contains("DISCO", StringComparison.CurrentCultureIgnoreCase));
            return isDisco;
        }

        private static DateTimeOffset GetCorrectDate(DateTimeOffset date, bool isDisco)
        {
            var discoCorrectedDate = date.Add(date.Offset.Negate());
            var dto = isDisco ? discoCorrectedDate : date;
            return dto;
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

        public Task<uint> GetFrameWidth()
        {
            return storageFile.GetProperty<uint>(FrameWidth);
        }

        public Task<uint> GetFrameHeight()
        {
            return storageFile.GetProperty<uint>(FrameHeight);
        }

        public Task<T> GetProperty<T>(string name)
        {
            return storageFile.GetProperty<T>(name);
        }

        public string FrameWidth => "System.Video.FrameWidth";
        public string FrameHeight => "System.Video.FrameHeight";
        public string DateEncoded => "System.Media.DateEncoded";
        public string Duration => "System.Media.Duration";
    }
}