using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class FlightContentViewModel : ReactiveObject
    {
        public IFile File { get; }
        public byte[] Thumbnail { get; }

        private FlightContentViewModel(byte[] thumbnail)
        {
            Thumbnail = thumbnail;
        }

        public ICommand PlayCommand { get; set; }

        public bool IsBusy { get; set; }

        public TimeSpan Duration { get; set; }

        public static async Task<FlightContentViewModel> Create(IFile file)
        {
            return new FlightContentViewModel(new byte[0]);
        }
    }
}