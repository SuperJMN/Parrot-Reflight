using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Parrot.FlightAcademy;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<List<FlightViewModel>> flights;
        private readonly ObservableAsPropertyHelper<bool> isBusy;

        public MainViewModel(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory, IMediaStore mediaStore)
        {
            LoadFlights = ReactiveCommand.CreateFromTask(() => LoadFlightsAsync(flightAcademyClientFactory, mediaStore));
            LoadFlights.ThrownExceptions.Subscribe(exception => { });
            flights = LoadFlights.ToProperty(this, x => x.Flights);
            isBusy = LoadFlights.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        private static async Task<List<FlightViewModel>> LoadFlightsAsync(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory,
            IMediaStore mediaMatcher)
        {
            var flightAcademyClient = await flightAcademyClientFactory();
            var observable = Observable.FromAsync(() => flightAcademyClient.GetFlights(0, 1500))
                .Select(x => x.Select(summary => new FlightViewModel(summary, mediaMatcher, flightAcademyClientFactory)).ToList());
            return await observable;
        }

        public List<FlightViewModel> Flights => flights.Value;

        public ReactiveCommand<Unit, List<FlightViewModel>> LoadFlights { get; }

        public FlightViewModel SelectedFlight { get; set; }

        public bool IsBusy => isBusy.Value;
    }
}