using System;
using System.Threading.Tasks;
using Reflight.ParrotApi;
using Xunit;

namespace Parrot_Reflight
{
    public class FlightAcademyClientTests
    {
        [Fact]
        public async Task GetFlights()
        {
            var client = await FlightAcademyClient.Create("superjmn@outlook.com", "blacksun");
            var flights = await client.GetFlights(0, 1);
        }
    }
}
