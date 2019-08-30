using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Reflight.Core;
using Reflight.ParrotApi;
using Reflight.Universal.Core.Filesystem;

namespace Reflight.Universal.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ContentMatcher mediaMatcher;
        private readonly ObservableAsPropertyHelper<List<FlightViewModel>> flights;
        private readonly ObservableAsPropertyHelper<bool> isBusy;

        public MainViewModel(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory, ContentMatcher mediaMatcher, SettingsViewModel settings)
        {
            this.mediaMatcher = mediaMatcher;
            LoadFlights = ReactiveCommand.CreateFromTask(() => LoadFlightsAsync(flightAcademyClientFactory, mediaMatcher));
            flights = LoadFlights.ToProperty(this, x => x.Flights);
            isBusy = LoadFlights.IsExecuting.ToProperty(this, x => x.IsBusy);
            Settings = settings;
        }

        private static async Task<List<FlightViewModel>> LoadFlightsAsync(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory,
            ContentMatcher mediaMatcher)
        {
            var flightAcademyClient = await flightAcademyClientFactory();
            var observable = Observable.FromAsync(() => flightAcademyClient.GetFlights(0, 1500))
                .Select(x => x.Select(summary => new FlightViewModel(summary, flightAcademyClient, mediaMatcher)).ToList());
            return await observable;
        }

        public List<FlightViewModel> Flights => flights.Value;

        public ReactiveCommand<Unit, List<FlightViewModel>> LoadFlights { get; }

        public FlightViewModel SelectedFlight { get; set; }

        public bool IsBusy => isBusy.Value;

        public SettingsViewModel Settings { get; set; }
    }
}