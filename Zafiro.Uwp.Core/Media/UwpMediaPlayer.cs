using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Windows.Media.Playback;
using Reflight.Gui.ViewModels;

namespace Zafiro.Uwp.Core.Media
{
    public class UwpMediaPlayer : IMediaPlayer, IMediaPlayerAdapterInjector
    {
        private readonly ISubject<double> playbackRateSubject;

        private readonly ISubject<TimeSpan> positionSubject;
        private MediaPlayer mediaPlayer;

        private IDisposable playbackRateChangedSubscription;
        private IDisposable positionChangedSubscription;

        public UwpMediaPlayer()
        {
            positionSubject = new Subject<TimeSpan>();
            playbackRateSubject = new Subject<double>();
        }

        public bool MediaPlayerAdapted => mediaPlayer != null;

        public IObservable<TimeSpan> Position => positionSubject.AsObservable();

        public void Play()
        {
            mediaPlayer.Play();
        }

        public void Pause()
        {
            mediaPlayer.Pause();
        }

        public void Adapt(MediaPlayer mediaPlayer)
        {
            if (this.mediaPlayer != null)
            {
                ReleaseMediaPlayer(this.mediaPlayer);
            }

            this.mediaPlayer = mediaPlayer;

            if (this.mediaPlayer != null)
            {
                SubscribeToMediaPlayerEvents(this.mediaPlayer);
            }

            OnMediaPlayerAdaptedChanged();
        }

        public event EventHandler MediaPlayerAdaptedChanged;

        protected virtual void OnMediaPlayerAdaptedChanged()
        {
            MediaPlayerAdaptedChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ReleaseMediaPlayer(MediaPlayer mediaPlayer)
        {
            using (playbackRateChangedSubscription)
            using (positionChangedSubscription)
            {
            }
        }

        private void SubscribeToMediaPlayerEvents(MediaPlayer mediaPlayer)
        {
            //Observable properties are backed up by subjects.
            //There is a possibility that my mediaplayer can change anytime, 
            //so I cannot expose Observable returned by FromEventPattern directly

            positionChangedSubscription = Observable
                .FromEventPattern<MediaPlaybackSession, object>(mediaPlayer.PlaybackSession,
                    nameof(mediaPlayer.PlaybackSession.PositionChanged))
                .Subscribe(x => positionSubject.OnNext(x.Sender.Position));

            playbackRateChangedSubscription = Observable
                .FromEventPattern<MediaPlaybackSession, object>(mediaPlayer.PlaybackSession,
                    nameof(mediaPlayer.PlaybackSession.PlaybackRateChanged))
                .Subscribe(x => playbackRateSubject.OnNext(x.Sender.PlaybackRate));
        }
    }
}