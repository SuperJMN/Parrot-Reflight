using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Reflight.ParrotApi;

namespace Reflight.Universal.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<List<FlightViewModel>> flights;
        private readonly ObservableAsPropertyHelper<bool> isBusy;

        public MainViewModel(IFlightAcademyClient flightAcademyClient)
        {
            LoadFlights = ReactiveCommand.CreateFromObservable(() => Observable(flightAcademyClient));
            flights = LoadFlights.ToProperty(this, x => x.Flights);
            isBusy = LoadFlights.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        private static IObservable<List<FlightViewModel>> Observable(IFlightAcademyClient flightAcademyClient)
        {
            return System.Reactive.Linq.Observable.FromAsync(() => flightAcademyClient.GetFlights(1, 15))
                .Select(x => x.Select(summary => new FlightViewModel(summary)).ToList());
        }

        public List<FlightViewModel> Flights => flights.Value;

        public ReactiveCommand<Unit, List<FlightViewModel>> LoadFlights { get; }

        public FlightViewModel SelectedFlight { get; set; }

        public bool IsBusy => isBusy.Value;
    }
}