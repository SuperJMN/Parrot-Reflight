﻿using System;
using System.Collections.Generic;
using ReactiveUI;

namespace Reflight.Gui.ViewModels
{
    public class PlottableViewModel : BasePlottableViewModel
    {
        private readonly ObservableAsPropertyHelper<Point> currentValue;

        public PlottableViewModel(IObservable<Point> valueObs, IList<double> values)
        {
            Values = values;
            currentValue = valueObs
                .ToProperty(this, x => x.CurrentValue);
        }

        public override IList<double> Values { get; }

        public override Point CurrentValue => currentValue.Value;
    }
}