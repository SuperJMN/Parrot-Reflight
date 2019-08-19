using System.Threading.Tasks;
using NodaTime;

namespace Reflight.Core
{
    public interface IVideoMetadataReader
    {
        Task<Interval> GetRecordedInterval(string path);
    }
}
