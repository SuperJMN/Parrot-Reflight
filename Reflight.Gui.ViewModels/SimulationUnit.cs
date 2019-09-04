using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class SimulationUnit
    {
        public Flight Flight { get; }
        public string Path { get; }

        public SimulationUnit(Flight flight, string path)
        {
            Flight = flight;
            Path = path;
        }
    }
}