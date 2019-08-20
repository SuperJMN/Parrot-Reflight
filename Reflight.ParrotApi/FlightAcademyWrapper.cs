using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Refit;
using Reflight.ParrotApi.Model;

namespace Reflight.ParrotApi
{
    public class FlightAcademyWrapper : IFlightAcademyClient
    {
        private readonly IFlightAcademyClient inner;

        public FlightAcademyWrapper(IFlightAcademyClient inner)
        {
            this.inner = inner;
        }

        public Task<ICollection<FlightSummary>> GetFlights(int page, int paginateBy)
        {
            return SafeCall(() => inner.GetFlights(page, paginateBy));
        }

        private static async Task<TOutput> SafeCall<TOutput>(Func<Task<TOutput>> func)
        {
            try
            {
                return await func();
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new InvalidLoginException("Your Parrot Flight Academy credentials seem to be incorrect.");
                }

                throw;
            }
        }

        public Task<FlightDetails> GetFlight(int id)
        {
            return SafeCall(() => inner.GetFlight(id));
        }
    }
}
