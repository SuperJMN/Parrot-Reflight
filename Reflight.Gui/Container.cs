using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Grace.DependencyInjection;
using Parrot.FlightAcademy;
using Reflight.Core;
using Reflight.Gui.ViewModels;
using Zafiro.Uwp.Core;
using Zafiro.Uwp.Core.Filesystem;
using Zafiro.Uwp.Core.Media;

namespace Reflight.Gui
{
    public static class Container
    {
        static Container()
        {
            var container = new DependencyInjectionContainer();
            container.Configure(x =>
            {
                x.Export<UwpFileSystem>().As<IFileSystem>();
                x.Export<DialogService>().As<IDialogService>();
                x.Export<MediaStore>().As<IMediaStore>();
                x.Export<UwpAccesibleFolders>().As<IAccesibleFolders>();
                x.Export<ViewModelFactory>().As<IViewModelFactory>();
                x.ExportFactory(() => new UwpSettingsStore(typeof(SettingsViewModel).Name,
                    typeof(SettingsViewModel), ApplicationData.Current.RoamingSettings)).As<ISettingsStore>();
                x.Export<Navigation>().As<INavigation>().Lifestyle.Singleton();
                x.Export<UwpMediaPlayer>().As<IMediaPlayer>().Lifestyle.Singleton();
                x.Export<VirtualDashboardRepository>().As<IVirtualDashboardRepository>().Lifestyle.Singleton();
                x.Export<SettingsViewModel>().As<ISettings>().As<SettingsViewModel>();

                x.ExportFactory((SettingsViewModel s) => FlightAcademyClient.Create(s.Username, s.Password));
                x.ExportFactory(() => new ResourceDictionary()
                    {Source = new Uri("ms-appx:///Views/VirtualDashboards.xaml")});

                x.ExcludeTypeFromAutoRegistration(typeof(Simulation));
            });
            
            Current = container;
        }

        public static DependencyInjectionContainer Current { get; }
    }
}