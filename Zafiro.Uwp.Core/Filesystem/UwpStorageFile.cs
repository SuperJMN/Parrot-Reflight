using System.Threading.Tasks;
using Windows.Storage;
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
    }
}