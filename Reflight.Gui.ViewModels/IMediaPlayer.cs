using System;

namespace Reflight.Gui.ViewModels
{
    public interface IMediaPlayer
    {
        void Play();
        void Pause();
        IObservable<TimeSpan> Position { get; }
    }
}