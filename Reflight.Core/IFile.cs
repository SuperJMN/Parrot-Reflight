using System;
using System.Threading.Tasks;
using NodaTime;

namespace Reflight.Core
{
    public interface IFile
    {
        string Path { get; }
        Task<byte[]> GetThumbnail();
        Task<Instant?> GetStart();
        Task<Duration?> GetDuration();
        Task<Interval?> GetInterval();
    }
}