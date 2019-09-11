using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Windows.Media.Playback;
using Reflight.Gui.ViewModels;

namespace Zafiro.Uwp.Core.Media
{
    public class UwpMediaPlayer : IMediaPlayer, IMediaPlayerAdapterInjector
    {
        private readonly ISubject<double> playbackRateSubject = new Subject<double>();
        private readonly ISubject<TimeSpan> positionSubject = new Subject<TimeSpan>();
        private readonly ISubject<TimeSpan> durationSubject = new Subject<TimeSpan>();

        private MediaPlayer mediaPlayer;
        private CompositeDisposable disposables = new CompositeDisposable();

        public IObservable<TimeSpan> Position => positionSubject.AsObservable();
        public IObservable<TimeSpan> Duration => durationSubject.AsObservable();

        public void SetPosition(TimeSpan timeSpan)
        {
            mediaPlayer.PlaybackSession.Position = timeSpan;
        }


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
            disposables?.Dispose();
            disposables = new CompositeDisposable();
        }

        private void SubscribeToMediaPlayerEvents(MediaPlayer mediaPlayer)
        {
            //Observable properties are backed up by subjects.
            //There is a possibility that my mediaplayer can change anytime, 
            //so I cannot expose Observable returned by FromEventPattern directly

            Observable
                .FromEventPattern<MediaPlaybackSession, object>(mediaPlayer.PlaybackSession,
                    nameof(mediaPlayer.PlaybackSession.PositionChanged))
                .Subscribe(x => positionSubject.OnNext(x.Sender.Position))
                .DisposeWith(disposables);

            Observable
                .FromEventPattern<MediaPlaybackSession, object>(mediaPlayer.PlaybackSession,
                    nameof(mediaPlayer.PlaybackSession.PlaybackRateChanged))
                .Subscribe(x => playbackRateSubject.OnNext(x.Sender.PlaybackRate))
                .DisposeWith(disposables);

            Observable
                .FromEventPattern<MediaPlaybackSession, object>(mediaPlayer.PlaybackSession,
                    nameof(mediaPlayer.PlaybackSession.NaturalDurationChanged))
                .Subscribe(x => durationSubject.OnNext(x.Sender.NaturalDuration))
                .DisposeWith(disposables);
        }
    }
}