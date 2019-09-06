using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ReactiveUI;
using Reflight.Core;
using Reflight.Gui.ViewModels;
using Reflight.Gui.Views.Pages;

namespace Reflight.Gui
{
    public class Navigation : INavigation
    {
        private Frame frame;

        readonly IDictionary<Type, Type> mappings = new Dictionary<Type, Type>()
        {
            { typeof(FlightReplayViewModel), typeof(SimulationPage) }
        };

        public Navigation()
        {
            MessageBus.Current.Listen<NavigationFrameMessage>().Subscribe(x => frame = x.Frame);
        }

        public Task Go<T>(object parameter = null)
        {
            var sourcePageType = mappings[typeof(T)];
            var vm = Container.Current.Locate<T>(parameter);
            frame.Navigate(sourcePageType, vm);
            return Task.CompletedTask;
        }

        public Task GoBack()
        {
            frame.GoBack();
            return Task.CompletedTask;
        }
    }
}