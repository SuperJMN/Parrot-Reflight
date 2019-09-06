using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Zafiro.Uwp.Core.Media
{
    public class SetMediaSourceBehavior : DependencyObject, IBehavior
    {
        public StorageFile SourceFile

        {
            get { return (StorageFile)GetValue(SourceFileProperty); }
            set { SetValue(SourceFileProperty, value); }
        }

        public static readonly DependencyProperty SourceFileProperty =
            DependencyProperty.Register("SourceFile", typeof(StorageFile), 
                typeof(SetMediaSourceBehavior), new PropertyMetadata(null, OnSourceFileChanged));

        private static void OnSourceFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SetMediaSourceBehavior setMediaSourceBehavior)
            {
                setMediaSourceBehavior.UpdateSource(e.NewValue as IStorageFile);
            }
        }

        private void UpdateSource(IStorageFile storageFile)
        {
            MediaPlayerElement.Source = null;

            if (storageFile != null)
            {
                var mediaSource = MediaSource.CreateFromStorageFile(storageFile);
                MediaPlayerElement.Source = mediaSource;
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
        }

        public DependencyObject AssociatedObject { get; private set; }
        public MediaPlayerElement MediaPlayerElement => (MediaPlayerElement) AssociatedObject;
    }
}