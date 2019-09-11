using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class FlightReplayViewModel : ReactiveObject
    {
        private readonly IMediaPlayer player;
        private readonly ISettings settings;
        private SimulationViewModel simulationViewModel;
        private readonly ObservableAsPropertyHelper<double> width;
        private readonly ObservableAsPropertyHelper<double> height;
        private double BaseSize = 1000;
        private bool isExpanded;

        public FlightReplayViewModel(Simulation simulation, bool isExpanded, INavigation navigation, IMediaPlayer player, ISettings settings)
        {
            this.player = player;
            this.settings = settings;
            Video = simulation.Video;
            SimulationViewModel = new SimulationViewModel(simulation, player.Position, simulation.PresentationOptions);
            
            Scale = 1D;
            var ratio = Video.Width / Video.Height;
            width = this.WhenAnyValue(x => x.Scale).Select(x => 1/x * ratio * BaseSize).ToProperty(this, x => x.Width);
            height = this.WhenAnyValue(x => x.Scale).Select(x => 1/x * BaseSize).ToProperty(this, x => x.Height);
            Play = ReactiveCommand.Create(() => MediaPlayer.Play());
            Expand = ReactiveCommand.CreateFromTask(() => navigation.Go<FlightReplayViewModel>(new {simulation, isExpanded = true }), Observable.Return(!isExpanded));
            Expand.Subscribe(_ => IsExpanded = true);
            GoBack = ReactiveCommand.CreateFromTask(navigation.GoBack);
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set => this.RaiseAndSetIfChanged(ref isExpanded, value);
        }

        public ReactiveCommand<Unit, Unit> GoBack { get; }

        public ReactiveCommand<Unit, Unit> Expand { get; }

        public ReactiveCommand<Unit, Unit> Play { get; }

        public double Width => width.Value;

        public double Height => height.Value;

        public double Scale
        {
            get => settings.DashboardScale;
            set
            {
                settings.DashboardScale = value;
                this.RaisePropertyChanged();
            }
        }

        public SimulationViewModel SimulationViewModel
        {
            get => simulationViewModel;
            set => this.RaiseAndSetIfChanged(ref simulationViewModel, value);
        }

        public Video Video { get; }

        public IMediaPlayer MediaPlayer => player;
    }
}