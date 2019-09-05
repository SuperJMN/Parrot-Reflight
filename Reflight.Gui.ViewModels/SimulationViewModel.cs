using System;
using System.Reactive;
using Parrot.FlightAcademy.Model;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class SimulationViewModel : ReactiveObject
    {
        public SimulationUnit SimulationUnit { get; }
        private readonly INavigation navigation;

        public SimulationViewModel(SimulationUnit simulationUnit, INavigation navigation)
        {
            SimulationUnit = simulationUnit;
            this.navigation = navigation;

            Close = ReactiveCommand.CreateFromTask(navigation.GoBack);
        }

        public ReactiveCommand<Unit, Unit> Close { get; }
    }
}