using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Microsoft.Xaml.Interactivity;

namespace Zafiro.Uwp.Core.Media
{
    public class InjectMediaPlayerBehavior : DependencyObject, IBehavior
    {
        public IMediaPlayerAdapterInjector MediaPlayerInjector
        {
            get { return (IMediaPlayerAdapterInjector)GetValue(MediaPlayerInjectorProperty); }
            set { SetValue(MediaPlayerInjectorProperty, value); }
        }

        public static readonly DependencyProperty MediaPlayerInjectorProperty =
            DependencyProperty.Register("MediaPlayerInjector", typeof(IMediaPlayerAdapterInjector), typeof(InjectMediaPlayerBehavior), new PropertyMetadata(null, OnMediaPlayerInjectorChanged));

        private static void OnMediaPlayerInjectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var injectMediaPlayerBehavior = d as InjectMediaPlayerBehavior;

            if (injectMediaPlayerBehavior != null)
                injectMediaPlayerBehavior.TryToInjectMediaPlayer();
        }

        public Windows.Media.Playback.MediaPlayer MediaPlayer
        {
            get { return (Windows.Media.Playback.MediaPlayer)GetValue(MediaPlayerProperty); }
            set { SetValue(MediaPlayerProperty, value); }
        }

        public static readonly DependencyProperty MediaPlayerProperty =
            DependencyProperty.Register("MediaPlayer", typeof(Windows.Media.Playback.MediaPlayer), typeof(InjectMediaPlayerBehavior), new PropertyMetadata(null, OnMediaPlayerChanged));

        private static void OnMediaPlayerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var injectMediaPlayerBehavior = d as InjectMediaPlayerBehavior;

            injectMediaPlayerBehavior?.TryToInjectMediaPlayer();
        }

        private void SetBindingOnLocalMediaPlayer()
        {
            Binding mediaPlayerBinding = new Binding
            {
                Source = MediaPlayerElement,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath(nameof(MediaPlayerElement.MediaPlayer))
            };

            BindingOperations.SetBinding(this, MediaPlayerProperty, mediaPlayerBinding);
        }

        private void TryToInjectMediaPlayer()
        {
            MediaPlayerInjector?.Adapt(MediaPlayer);
        }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            if (ReadLocalValue(MediaPlayerProperty) == DependencyProperty.UnsetValue)
                SetBindingOnLocalMediaPlayer();
        }

        public void Detach()
        {
        }

        public DependencyObject AssociatedObject { get; set; }
        public MediaPlayerElement MediaPlayerElement => (MediaPlayerElement) AssociatedObject;
    }
}