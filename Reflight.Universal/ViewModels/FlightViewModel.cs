using System;
using Reflight.ParrotApi;
using Reflight.ParrotApi.Model;

namespace Reflight.Universal.ViewModels
{
    public class FlightViewModel
    {
        private readonly FlightSummary summary;

        public FlightViewModel(FlightSummary summary)
        {
            this.summary = summary;
        }

        public DateTimeOffset RecordedDate => summary.Date;

        public TimeSpan TotalRunTime => summary.TotalRunTime;
    }
}