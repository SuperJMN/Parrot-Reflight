using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Parrot.FlightAcademy;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class FlightGalleryViewModel : ReactiveObject
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IDialogService dialogService;
        private readonly ObservableAsPropertyHelper<IList<FlightViewModel>> flights;
        private readonly ObservableAsPropertyHelper<bool> isBusy;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public FlightGalleryViewModel(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory, IViewModelFactory viewModelFactory, IDialogService dialogService)
        {
            this.viewModelFactory = viewModelFactory;
            this.dialogService = dialogService;
            LoadFlights = ReactiveCommand.CreateFromTask(() => LoadFlightsAsync(flightAcademyClientFactory));
            var thrownExceptions = LoadFlights.ThrownExceptions.SelectMany(Handle);
            thrownExceptions.Subscribe().DisposeWith(disposables);
            flights = LoadFlights.ToProperty(this, x => x.Flights);
            isBusy = LoadFlights.IsExecuting.ToProperty(this, x => x.IsBusy);
        }

        private async Task<Unit> Handle(Exception exception)
        {
            if (exception is InvalidCredentialException)
            {
                await dialogService.ShowError("Empty credentials",
                    "Please, specify your Flight Academy account under the Settings section");
            }

            if (exception is InvalidLoginException)
            {
                await dialogService.ShowError("Invalid credentials",
                    "Please, review your Flight Academy account under the Settings section");
            }

            return Unit.Default;
        }

        private async Task<IList<FlightViewModel>> LoadFlightsAsync(Func<Task<IFlightAcademyClient>> flightAcademyClientFactory)
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