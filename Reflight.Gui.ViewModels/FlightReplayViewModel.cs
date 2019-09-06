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
        private SimulationViewModel simulationViewModel;

        public FlightReplayViewModel(Simulation simulation, INavigation navigation, IMediaPlayer player)
        {
            this.player = player;
            Video = simulation.Video;
            SimulationViewModel = new SimulationViewModel(simulation, player.Position, simulation.PresentationOptions);
            Close = ReactiveCommand.CreateFromTask(navigation.GoBack);
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