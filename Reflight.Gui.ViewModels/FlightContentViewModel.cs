using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using NodaTime;
using Parrot.FlightAcademy;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class FlightContentViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<bool> isBusy;
        public byte[] Thumbnail { get; }
        public string Path { get; }

        public FlightContentViewModel(int flightId, byte[] thumbnail, string path,
            Func<Task<IFlightAcademyClient>> clientFactory, INavigation navigation)
        {
            Thumbnail = thumbnail;
            Path = path;
            PlayCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var flightDetails = await (await clientFactory()).GetFlight(flightId);
                var flight = flightDetails.ToFlight();
                var simulationUnit = new SimulationUnit(flight, path);
                await navigation.Go<SimulationViewModel>(new { simulationUnit = simulationUnit });
                return simulationUnit;
            });
            
            isBusy = PlayCommand.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        public ReactiveCommand<Unit, SimulationUnit> PlayCommand { get; }


        public bool IsBusy => isBusy.Value;

        public Duration? Duration { get; set; }
    }
}