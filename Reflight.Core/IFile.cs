using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface IFile
    {
        string Path { get; }
        Task<byte[]> GetThumbnail();
    }
}