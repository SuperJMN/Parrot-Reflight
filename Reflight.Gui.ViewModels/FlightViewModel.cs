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
        private readonly ObservableAsPropertyHelper<IList<FlightContentViewModel>> items;
        private readonly ObservableAsPropertyHelper<bool> isBusy;

        public FlightViewModel(FlightSummary summary, IMediaStore matcher, IViewModelFactory viewModelFactory)
        {
            this.summary = summary;

            LoadItems = ReactiveCommand.CreateFromObservable(() => matcher
                .RecordingsBetween(this.summary.Date.ToInterval(this.summary.TotalRunTime))
                .SelectMany(file => viewModelFactory.CreateFlightContentViewModel(summary.Id, file)).ToList());
            items = LoadItems.ToProperty(this, x => x.Items);
            isBusy = LoadItems.IsExecuting.ToProperty(this, x => x.IsBusy);
            Play = ReactiveCommand.CreateFromObservable((FlightContentViewModel e) => e.PlayCommand.Execute());
        }

        public ReactiveCommand<FlightContentViewModel, Simulation> Play { get; }

        public DroneModel Model => DroneModel.FromProductId(summary.ProductId);

        public ReactiveCommand<Unit, IList<FlightContentViewModel>> LoadItems { get; }

        public DateTimeOffset RecordedDate => summary.Date;

        public TimeSpan TotalRunTime => summary.TotalRunTime;

        public IList<FlightContentViewModel> Items => items.Value;

        public IFile SelectedItem { get; set; }

        public bool IsBusy => isBusy.Value;

        
    }
}