using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Search;
using Reflight.Core;

namespace Reflight.Universal.Core.Filesystem
{
    public class UwpFileSystem : IFileSystem
    {
        public async Task<T> GetMetadata<T>(string path, string property)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            return await file.GetProperty<T>(property);
        }

        public IMetadataProperties Metadata => new UwpMetadata();
        public IObservable<string> GetAllFiles()
        {
            var storageItemAccessList = StorageApplicationPermissions.FutureAccessList;
            var files = storageItemAccessList.Entries.Select(entry => entry.Token).ToObservable()
                .SelectMany(storageItemAccessList.GetFolderAsync)
                .SelectMany(x => x.GetFilesAsync(CommonFileQuery.DefaultQuery))
                .SelectMany(x => x)
                .Select(x => x.Path);

            return files;
        }
    }
}