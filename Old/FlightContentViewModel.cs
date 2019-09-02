using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using ReactiveUI;
using Reflight.Universal.Core;

namespace Reflight.Universal
{
    public class FlightContentViewModel : ReactiveObject
    {
        public StorageFile File { get; }
        public byte[] Thumbnail { get; }

        private FlightContentViewModel(StorageFile file, byte[] thumbnail)
        {
            File = file;
            Thumbnail = thumbnail;
        }

        public ICommand PlayCommand { get; set; }

        public bool IsBusy { get; set; }

        public TimeSpan Duration { get; set; }

        public static async Task<FlightContentViewModel> Create(StorageFile file)
        {
            return new FlightContentViewModel(file, await file.GetThumbnailBytes());
        }
    }
}