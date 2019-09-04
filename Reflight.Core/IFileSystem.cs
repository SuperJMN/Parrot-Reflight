using System;
using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface IFileSystem
    {
        IObservable<IFile> GetAllFiles();
        Task<IFile> GetFile(string path);
    }
}