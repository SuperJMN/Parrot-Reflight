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
        private double factor;
        private readonly ObservableAsPropertyHelper<double> width;
        private ObservableAsPropertyHelper<double> height;
        private double BaseSize = 1000;

        public FlightReplayViewModel(Simulation simulation, INavigation navigation, IMediaPlayer player, ISettings settings)
        {
            this.player = player;
            this.settings = settings;
            Video = simulation.Video;
            SimulationViewModel = new SimulationViewModel(simulation, player.Position, simulation.PresentationOptions);
            Close = ReactiveCommand.CreateFromTask(navigation.GoBack);
            Factor = 1D;
            var ratio = Video.Width / Video.Height;
            width = this.WhenAnyValue(x => x.Factor).Select(x => x * ratio * BaseSize).ToProperty(this, x => x.Width);
            height = this.WhenAnyValue(x => x.Factor).Select(x => x * BaseSize).ToProperty(this, x => x.Height);
            Play = ReactiveCommand.Create(() => MediaPlayer.Play());
        }

        public ReactiveCommand<Unit, Unit> Play { get; }

        public double Width => width.Value;

        public double Height => height.Value;

        public double Factor
        {
            get => settings.DashboardScale;
            set
            {
                settings.DashboardScale = value;
                this.RaisePropertyChanged();
            }
        }

        public ReactiveCommand<Unit, Unit> Close { get; }

        public SimulationViewModel SimulationViewModel
        {
            get => simulationViewModel;
            set => this.RaiseAndSetIfChanged(ref simulationViewModel, value);
        }

        public Video Video { get; }

        public IMediaPlayer MediaPlayer => player;
    }
}