using System;
using System.IO;
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

        public IObservable<string> GetFilesFromFolder(string folder)
        {
            var filesFromFolder = fileSystem
                .ToObservable()
                .Where(x => string.Equals(Path.GetDirectoryName(x.Item1), folder, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Item1);

            return filesFromFolder;
        }

        public Task<T> GetMetadata<T>(string file, string property)
        {
            var entry = fileSystem.FirstOrDefault(x => x.path == file);

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
    }
}