using System;
using System.Threading.Tasks;
using Parrot.FlightAcademy;
using Parrot.FlightAcademy.Model;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly Func<Task<IFlightAcademyClient>> clientFactory;
        private readonly IMediaStore mediaStore;
        private readonly INavigation navigation;
        private readonly ISettings settings;

        public ViewModelFactory(Func<Task<IFlightAcademyClient>> clientFactory, IMediaStore mediaStore, INavigation navigation, ISettings settings)
        {
            this.clientFactory = clientFactory;
            this.mediaStore = mediaStore;
            this.navigation = navigation;
            this.settings = settings;
        }

        public async Task<FlightContentViewModel> CreateFlightContentViewModel(int flightId, IFile file)
        {
            var flightContentViewModel = new FlightContentViewModel(flightId, await file.GetThumbnail(), file, clientFactory, navigation, settings);
            flightContentViewModel.Duration = await file.GetDuration();
            return flightContentViewModel;
        }

        public Task<FlightViewModel> CreateFlightViewModel(FlightSummary summary)
        {
            return Task.FromResult(new FlightViewModel(summary, mediaStore, this));
        }
    }
}