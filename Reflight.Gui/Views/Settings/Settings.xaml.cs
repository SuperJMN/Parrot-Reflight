using Windows.UI.Xaml.Controls;
using Reflight.Gui.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Reflight.Gui.Views.Settings
{
    public sealed partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            DataContext = Container.Current.Locate<SettingsViewModel>();
        }
    }
}
