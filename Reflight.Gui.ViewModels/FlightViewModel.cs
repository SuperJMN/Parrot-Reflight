using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Parrot.FlightAcademy.Model;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class FlightViewModel : ReactiveObject
    {
        private readonly FlightSummary summary;
        private readonly IMediaStore matcher;
        private readonly ObservableAsPropertyHelper<IList<FlightContentViewModel>> items;

        public FlightViewModel(FlightSummary summary, IMediaStore matcher)
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
            return matcher.RecordingsBetween(summary.Date.ToInterval(summary.TotalRunTime))
                .SelectMany(FlightContentViewModel.Create);
        }

        public DateTimeOffset RecordedDate => summary.Date;

        public TimeSpan TotalRunTime => summary.TotalRunTime;

        public IList<FlightContentViewModel> Items => items.Value;

        public IFile SelectedItem { get; set; }
    }
}