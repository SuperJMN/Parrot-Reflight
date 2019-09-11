using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace Reflight.Gui.Views.Controls
{
    public sealed partial class Donation : UserControl
    {
        public Donation()
        {
            this.InitializeComponent();
        }

        private async void DonateOnTap(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://paypal.me/SuperJMN"));
        }
    }
}
