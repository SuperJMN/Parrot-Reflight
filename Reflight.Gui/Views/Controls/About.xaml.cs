using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;


namespace Reflight.Gui.Views.Controls
{
    public sealed partial class About
    {
        public About()
        {
            this.InitializeComponent();
        }

        private async void Hyperlink_OnClick(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/SuperJMN/Parrot-Reflight"));
        }
    }
}
