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

        public ViewModelFactory(Func<Task<IFlightAcademyClient>> clientFactory, IMediaStore mediaStore, INavigation navigation)
        {
            this.clientFactory = clientFactory;
            this.mediaStore = mediaStore;
            this.navigation = navigation;
        }

        public async Task<FlightContentViewModel> CreateFlightContentViewModel(int flightId, IFile file)
        {
            var flightContentViewModel = new FlightContentViewModel(flightId, await file.GetThumbnail(), file.Path, clientFactory, navigation);
            flightContentViewModel.Duration = await file.GetDuration();
            return flightContentViewModel;
        }

        public Task<FlightViewModel> CreateFlightViewModel(FlightSummary summary)
        {
            return Task.FromResult(new FlightViewModel(summary, mediaStore, this));
        }
    }
}