using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reflight.Core.Tests
{
    public class TestFileSystem : IFileSystem
    {
        private readonly (string path, DateTimeOffset? encodedDate, ulong duration)[] fileSystem;

        public TestFileSystem((string, DateTimeOffset?, ulong duration)[] fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Task<T> GetMetadata<T>(string path, string property)
        {
            var entry = fileSystem.FirstOrDefault(x => x.path == path);

            if (entry == default)
            {
                return Task.FromResult(default(T));
            }

            if (property == Metadata.DateEncoded)
            {
                dynamic prop = entry.encodedDate;
                return Task.FromResult<T>(prop);
            }

            if (property == Metadata.Duration)
            {
                dynamic prop = entry.duration;
                return Task.FromResult<T>(prop);
            }

            throw new InvalidOperationException();
        }

        public IMetadataProperties Metadata => new TestMetadata();
        public IObservable<string> GetAllFiles()
        {
            return fileSystem.ToObservable()
                .Select((x, y) => x.path);
        }
    }
}