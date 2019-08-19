using System;
using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface IFileSystem
    {
        IObservable<string> GetFilesFromFolder(string folder);
        Task<T> GetMetadata<T>(string file, string property);
        IMetadataProperties Metadata { get; }
    }

    public interface IMetadataProperties
    {
        string Duration { get; }
        string DateEncoded { get; }
    }
}