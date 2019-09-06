using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Reflight.Gui.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Reflight.Gui.Views.Controls
{
    public sealed partial class FlightViewerControl
    {
        public static readonly DependencyProperty FlightReplayViewModelProperty = DependencyProperty.Register("FlightReplayViewModel", typeof(SimulationViewModel), typeof(FlightViewerControl), new PropertyMetadata(default(SimulationViewModel), OnReplayChanged));

        private static void OnReplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (FlightViewerControl) d;
            
        }

        public FlightViewerControl()
        {
            this.InitializeComponent();
        }

        public FlightReplayViewModel FlightReplayViewModel
        {
            get { return (FlightReplayViewModel) GetValue(FlightReplayViewModelProperty); }
            set { SetValue(FlightReplayViewModelProperty, value); }
        }
    }
}
