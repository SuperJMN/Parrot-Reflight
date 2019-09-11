using System.Collections.Generic;
using Reflight.Core;

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