using System;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui.ViewModels
{
    public interface ISimulationViewModel
    {
        StatusViewModel Status { get; }
        PresentationOptions PresentationOptions { get; }
        IPlottableViewModel Speed { get; }
        IPlottableViewModel Altitude { get; }
        TimeSpan FlightDuration { get; }
        TimeSpan CapturedDuration { get; }
    }
}