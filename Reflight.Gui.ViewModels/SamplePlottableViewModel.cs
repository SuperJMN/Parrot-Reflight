using System.Collections.Generic;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    internal class SamplePlottableViewModel : BasePlottableViewModel
    {
        public SamplePlottableViewModel(IList<double> values, Point currentValue)
        {
            Values = values;
            CurrentValue = currentValue;
        }

        public override IList<double> Values { get; }
        public override Point CurrentValue { get; }
    }
}