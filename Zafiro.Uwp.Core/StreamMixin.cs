using System.IO;
using System.Threading.Tasks;

namespace Zafiro.Uwp.Core
{
    public static class StreamMixin
    {
        public static async Task<byte[]> ToByteArray(this Stream readStream)
        {
            var byteArray = new byte[readStream.Length];
            await readStream.ReadAsync(byteArray, 0, byteArray.Length);
            return byteArray;
        }
    }
}