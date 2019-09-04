using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Search;
using Reflight.Core;

namespace Zafiro.Uwp.Core.Filesystem
{
    public class UwpFileSystem : IFileSystem
    {
        IObservable<IFile> IFileSystem.GetAllFiles()
        {
            var storageItemAccessList = StorageApplicationPermissions.FutureAccessList;
            var files = storageItemAccessList.Entries.Select(entry => entry.Token).ToObservable()
                .SelectMany(storageItemAccessList.GetFolderAsync)
                .SelectMany(x => x.GetFilesAsync(CommonFileQuery.DefaultQuery))
                .SelectMany(x => x)
                .Select(x => new UwpStorageFile(x));

            return files;
        }

        public async Task<IFile> GetFile(string path)
        {
            return new UwpStorageFile(await StorageFile.GetFileFromPathAsync(path));
        }
    }
}