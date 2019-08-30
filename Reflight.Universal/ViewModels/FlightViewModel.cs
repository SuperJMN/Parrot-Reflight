using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using ReactiveUI;
using Reflight.Core;
using Reflight.ParrotApi;
using Reflight.ParrotApi.Model;
using Reflight.Universal.Core.Filesystem;

namespace Reflight.Universal.ViewModels
{
    public class FlightViewModel : ReactiveObject
    {
        private readonly FlightSummary summary;
        private readonly ContentMatcher matcher;
        private readonly ObservableAsPropertyHelper<IList<FlightContentViewModel>> items;

        public FlightViewModel(FlightSummary summary, IFlightAcademyClient flightAcademyClient, ContentMatcher matcher)
        {
            this.summary = summary;
            this.matcher = matcher;

            LoadItems = ReactiveCommand.CreateFromObservable(() => Load().ToList());
            LoadItems.ThrownExceptions.Subscribe(x => { });
            items = LoadItems.ToProperty(this, x => x.Items);
        }

        public ReactiveCommand<Unit, IList<FlightContentViewModel>> LoadItems { get; }

        private IObservable<FlightContentViewModel> Load()
        {
            return matcher.Match(summary.Date.ToInterval(summary.TotalRunTime))
                .SelectMany(FlightContentViewModel.Create);
        }

        public DateTimeOffset RecordedDate => summary.Date;

        public TimeSpan TotalRunTime => summary.TotalRunTime;

        public IList<FlightContentViewModel> Items => items.Value;

        public StorageFile SelectedItem { get; set; }
    }
}