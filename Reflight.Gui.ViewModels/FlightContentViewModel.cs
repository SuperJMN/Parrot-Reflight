using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using NodaTime;
using Parrot.FlightAcademy;
using ReactiveUI;
using Reflight.Core;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui.ViewModels
{
    public class FlightContentViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<bool> isBusy;
        public byte[] Thumbnail { get; }
        public IFile Source { get; }

        public FlightContentViewModel(int flightId, byte[] thumbnail, IFile source,
            Func<Task<IFlightAcademyClient>> clientFactory, INavigation navigation, ISettings settings)
        {
            Thumbnail = thumbnail;
            Source = source;
            PlayCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var flightDetails = await (await clientFactory()).GetFlight(flightId);
                var flight = flightDetails.ToFlight();
                var video = await Video.Load(source);
                var simulationUnit = new Simulation(video, flight, new PresentationOptions()
                {
                    Dashboard = settings.VirtualDashboard,
                    UnitPack = settings.UnitPack,
                });

                await navigation.Go<FlightReplayViewModel>(new { simulationUnit, isExpanded = false });
                return simulationUnit;
            });
            
            isBusy = PlayCommand.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        public ReactiveCommand<Unit, Simulation> PlayCommand { get; }


        public bool IsBusy => isBusy.Value;

        public Duration? Duration { get; set; }
    }
}