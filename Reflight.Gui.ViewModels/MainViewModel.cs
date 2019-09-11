using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly INavigation navigation;
        private readonly ObservableAsPropertyHelper<bool> canGoBack;

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            GoToGallery = ReactiveCommand.CreateFromTask((object _) => navigation.Go<FlightGalleryViewModel>());
            ItemInvoked = ReactiveCommand.Create<Section>(NavigateTo);
            Sections = new List<Section>
            {
                new Section("Gallery"),
                new Section("About"),
                new Section("Donation"),
            };

            canGoBack = navigation.CanGoBack.ToProperty(this, x => x.CanGoBack);
            GoBack = ReactiveCommand.Create(navigation.GoBack);
            SelectedItem = Sections.First();
        }

        public ReactiveCommand<Unit, Task> GoBack { get; }

        private void NavigateTo(Section s)
        {
            switch (s.Name)
            {
                case "Gallery":
                    navigation.Go<FlightGalleryViewModel>();
                    break;
                case "About":
                    navigation.Go<AboutViewModel>();
                    break;
                case "Donation":
                    navigation.Go<DonationViewModel>();
                    break;
                case "Settings":
                    navigation.Go<SettingsViewModel>();
                    break;
            }
        }

        public ReactiveCommand<Section, Unit> ItemInvoked { get; set; }

        public ReactiveCommand<object, Unit> GoToGallery { get; }

        public IEnumerable<Section> Sections { get; }

        public bool CanGoBack => canGoBack.Value;

        public object SelectedItem { get; set; }
    }
}