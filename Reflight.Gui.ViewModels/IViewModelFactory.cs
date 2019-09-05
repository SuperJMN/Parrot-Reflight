using System.Threading.Tasks;
using Parrot.FlightAcademy.Model;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public interface IViewModelFactory
    {
        Task<FlightContentViewModel> CreateFlightContentViewModel(int flightId, IFile file);
        Task<FlightViewModel> CreateFlightViewModel(FlightSummary summary);
    }
}