using System;
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
        private readonly Func<Task<IFlightAcademyClient>> clientFactory;
        private readonly ObservableAsPropertyHelper<bool> isBusy;
        public byte[] Thumbnail { get; }
        public string Path { get; }

        private FlightContentViewModel(int flightId, byte[] thumbnail, string path, Func<Task<IFlightAcademyClient>> clientFactory)
        {
            this.clientFactory = clientFactory;
            Thumbnail = thumbnail;
            Path = path;
            PlayCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var flightDetails = await (await clientFactory()).GetFlight(flightId);
                var flight = flightDetails.ToFlight();
                return new SimulationUnit(flight, path);
            });
            PlayCommand.Subscribe(simulationUnit => { });
            isBusy = PlayCommand.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        public ReactiveCommand<Unit, SimulationUnit> PlayCommand { get; }


        public bool IsBusy => isBusy.Value;

        public Duration? Duration { get; set; }

        public static async Task<FlightContentViewModel> Create(int flightId, IFile file, Func<Task<IFlightAcademyClient>> clientFactory)
        {
            var flightContentViewModel = new FlightContentViewModel(flightId, await file.GetThumbnail(), file.Path, clientFactory);
            flightContentViewModel.Duration = await file.GetDuration();
            return flightContentViewModel;
        }
    }
}