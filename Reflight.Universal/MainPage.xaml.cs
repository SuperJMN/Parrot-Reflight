using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Reflight.ParrotApi;
using Reflight.Universal.Core.Filesystem;
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

            var matcher = new ContentMatcher();

            DataContext = new MainViewModel(FactoryClient, matcher, settingsViewModel);

            base.OnNavigatedTo(e);
        }
    }
}
