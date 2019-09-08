using System;
using ReactiveUI;
using Reflight.Core;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui.ViewModels
{
    public class DefaultSimulationViewModel : ReactiveObject, ISimulationViewModel
    {
        private StatusViewModel status;

        public DefaultSimulationViewModel(PresentationOptions presentationOptions)
        {
            PresentationOptions = presentationOptions;
        }

        public UnitPack Units { get; set; }

        public StatusViewModel Status
        {
            get => status;
            set => this.RaiseAndSetIfChanged(ref status, value);
        }

        public PresentationOptions PresentationOptions { get; }
        public IPlottableViewModel Speed { get; set; }
        public IPlottableViewModel Altitude { get; set; }
        public TimeSpan FlightDuration { get; }
        public TimeSpan CapturedDuration { get; }
        public DroneModel DroneModel => DroneModel.Disco;
    }
}