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
        private readonly IViewModelFactory viewModelFactory;
        private readonly ObservableAsPropertyHelper<IList<FlightViewModel>> flights;
        private readonly ObservableAsPropertyHelper<bool> isBusy;

        public MainViewModel(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory, IMediaStore mediaStore, IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            LoadFlights = ReactiveCommand.CreateFromTask(() => LoadFlightsAsync(flightAcademyClientFactory, mediaStore));
            LoadFlights.ThrownExceptions.Subscribe(exception => { });
            flights = LoadFlights.ToProperty(this, x => x.Flights);
            isBusy = LoadFlights.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        private async Task<IList<FlightViewModel>> LoadFlightsAsync(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory,
            IMediaStore mediaMatcher)
        {
            var flightAcademyClient = await flightAcademyClientFactory();
            var observable = Observable
                .FromAsync(() => flightAcademyClient.GetFlights(0, 1500))
                .Select(x => x.ToObservable().SelectMany(summary => viewModelFactory.CreateFlightViewModel(summary)).ToList());
            var observable1 = await observable;
            var flightViewModels = await observable1;
            return flightViewModels;
        }

        public IList<FlightViewModel> Flights => flights.Value;

        public ReactiveCommand<Unit, IList<FlightViewModel>> LoadFlights { get; }

        public FlightViewModel SelectedFlight { get; set; }

        public bool IsBusy => isBusy.Value;
    }
}