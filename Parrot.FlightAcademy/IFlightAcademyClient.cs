using System.Collections.Generic;
using System.Threading.Tasks;
using Parrot.FlightAcademy.Model;
using Refit;

namespace Parrot.FlightAcademy
{
    public interface IFlightAcademyClient
    {
        [Get("/runs")]
        Task<ICollection<FlightSummary>> GetFlights(int page, [AliasAs("paginate_by")]int paginateBy);

        [Get("/runs/{id}/details")]
        Task<FlightDetails> GetFlight([AliasAs("id")] int id);
    }
}