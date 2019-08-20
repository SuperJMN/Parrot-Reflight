using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Reflight.Core;
using Reflight.ParrotApi;
using Reflight.ParrotApi.Model;

namespace Reflight.Universal.ViewModels
{
    public class FlightViewModel
    {
        private readonly FlightSummary summary;
        private readonly IVideoMediaMatcher matcher;

        public FlightViewModel(FlightSummary summary, IFlightAcademyClient flightAcademyClient, IVideoMediaMatcher matcher)
        {
            this.summary = summary;
            this.matcher = matcher;

            LoadItems = ReactiveCommand.CreateFromTask(Load);
        }

        public ReactiveCommand<Unit, IEnumerable<FlightContentPart>> LoadItems { get; }

        private async Task<IEnumerable<FlightContentPart>> Load()
        {
            var items = await matcher.Lookup(summary.Date.ToInterval(summary.TotalRunTime), new List<string> {"E:\\Local\\Parrot\\Anafi", "E:\\Local\\Parrot\\Disco"}).ToList();

            return items.Select(FlightContentPart.FromFile);
        }

        public DateTimeOffset RecordedDate => summary.Date;

        public TimeSpan TotalRunTime => summary.TotalRunTime;

        public List<FlightContentPart> Items { get; set; }

        public FlightContentPart SelectedItem { get; set; }
    }
}