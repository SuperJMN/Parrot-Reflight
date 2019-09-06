using Windows.Media.Playback;

namespace Zafiro.Uwp.Core.Media
{
    public interface IMediaPlayerAdapterInjector
    {
        void Adapt(Windows.Media.Playback.MediaPlayer mediaPlayer);
    }
}