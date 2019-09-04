using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Parrot.FlightAcademy;
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

        public FlightViewModel(FlightSummary summary, IMediaStore matcher,
            Func<Task<IFlightAcademyClient>> clientFactory)
        {
            this.summary = summary;

            LoadItems = ReactiveCommand.CreateFromObservable(() => matcher
                .RecordingsBetween(this.summary.Date.ToInterval(this.summary.TotalRunTime))
                .SelectMany(file => FlightContentViewModel.Create(summary.Id, file, clientFactory)).ToList());
            items = LoadItems.ToProperty(this, x => x.Items);
            isBusy = LoadItems.IsExecuting.ToProperty(this, x => x.IsBusy);
            Play = ReactiveCommand.CreateFromObservable((FlightContentViewModel e) => e.PlayCommand.Execute());
        }

        public ReactiveCommand<FlightContentViewModel, SimulationUnit> Play { get; }

        public DroneModel Model => DroneModel.FromProductId(summary.ProductId);

        public ReactiveCommand<Unit, IList<FlightContentViewModel>> LoadItems { get; }

        public DateTimeOffset RecordedDate => summary.Date;

        public TimeSpan TotalRunTime => summary.TotalRunTime;

        public IList<FlightContentViewModel> Items => items.Value;

        public IFile SelectedItem { get; set; }

        public bool IsBusy => isBusy.Value;

        
    }
}