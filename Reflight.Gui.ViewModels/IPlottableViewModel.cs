using System.Collections.Generic;

namespace Reflight.Gui.ViewModels
{
    public interface IPlottableViewModel
    {
        IList<double> Values { get; }
        IList<double> SampledValues { get; }
        double Maximum { get; }
        Point CurrentValue { get; }
    }
}