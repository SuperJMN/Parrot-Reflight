using System;
using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface IFileSystem
    {
        Task<T> GetMetadata<T>(string path, string property);
        IMetadataProperties Metadata { get; }
        IObservable<string> GetAllFiles();
    }
}