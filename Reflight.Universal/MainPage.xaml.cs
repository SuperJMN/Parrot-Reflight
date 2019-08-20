using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Reflight.ParrotApi;
using Reflight.Universal.Core;
using Reflight.Universal.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reflight.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var settingsViewModel = new SettingsViewModel();
            Task<IFlightAcademyClient> FactoryClient() => FlightAcademyClient.Create(settingsViewModel.Username,
                settingsViewModel.Password);

            DataContext = new MainViewModel(FactoryClient, new UwpContentMatcher(), settingsViewModel);

            base.OnNavigatedTo(e);
        }
    }
}
