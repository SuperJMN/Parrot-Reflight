using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace Zafiro.Uwp.Core.Controls
{
    public class LabeledRadialGauge : RadialGauge
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(LabeledRadialGauge), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
    }
}