using System;
using System.Windows.Input;
using ReactiveUI;

namespace Reflight.Universal
{
    public class FlightContentViewModel : ReactiveObject
    {
        public ICommand PlayCommand { get; set; }

        public bool IsBusy { get; set; }

        public TimeSpan Duration { get; set; }
    }
}