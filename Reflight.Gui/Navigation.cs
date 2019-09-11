using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ReactiveUI;
using Reflight.Core;
using Reflight.Gui.ViewModels;
using Reflight.Gui.Views.Controls;
using Reflight.Gui.Views.Pages;
using Reflight.Gui.Views.Settings;

namespace Reflight.Gui
{
    public class Navigation : INavigation
    {
        private Frame innerFrame;

        readonly IDictionary<Type, Type> mappings = new Dictionary<Type, Type>
        {
            { typeof(FlightReplayViewModel), typeof(SimulationControl) },
            { typeof(FlightGalleryViewModel), typeof(FlightGallery) },
            { typeof(SettingsViewModel), typeof(Settings) },
            { typeof(DonationViewModel), typeof(Donation) },
            { typeof(AboutViewModel), typeof(About) },
        };

        private readonly ISubject<bool> canGoBack = new BehaviorSubject<bool>(false);
        private IDisposable subscription;
        
        public Navigation()
        {
            MessageBus.Current.Listen<NavigationFrameMessage>().Subscribe(x =>
            {
                subscription?.Dispose();
                innerFrame = x.InnerFrame;

                var obs = Observable.FromEventPattern<NavigatedEventHandler, NavigationEventArgs>(h => innerFrame.Navigated += h, h => innerFrame.Navigated -= h)
                    .Select(_ => innerFrame.CanGoBack);

                subscription = obs.Subscribe(canGoBack);
            });
        }

        public Task Go<T>(object parameter = null)
        {
            return GoCore<T>(innerFrame, parameter);
        }

        private Task GoCore<T>(Frame frame, object parameter)
        {
            var controlType = mappings[typeof(T)];
            var vm = Container.Current.Locate<T>(parameter);
            var control = Container.Current.Locate(controlType);
            frame.Navigate(typeof(NavigationPage), ((object)control, (object)vm));
            return Task.CompletedTask;
        }

        public Task GoBack()
        {
            innerFrame.GoBack();
            return Task.CompletedTask;
        }

        public IObservable<bool> CanGoBack => canGoBack;
    }
}
