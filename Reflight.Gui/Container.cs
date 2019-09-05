using Windows.Storage;
using Grace.DependencyInjection;
using Parrot.FlightAcademy;
using Reflight.Core;
using Reflight.Gui.ViewModels;
using Zafiro.Uwp.Core;
using Zafiro.Uwp.Core.Filesystem;

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
                x.Export<MediaStore>().As<IMediaStore>();
                x.Export<UwpAccesibleFolders>().As<IAccesibleFolders>();
                x.Export<ViewModelFactory>().As<IViewModelFactory>();
                x.ExportFactory(() => new UwpSettingsStore(typeof(SettingsViewModel).Name,
                    typeof(SettingsViewModel), ApplicationData.Current.RoamingSettings)).As<ISettingsStore>();
                x.Export<Navigation>().As<INavigation>().Lifestyle.Singleton();
                x.Export<MainViewModel>();
                x.ExportFactory((SettingsViewModel s) => FlightAcademyClient.Create(s.Username, s.Password));

                x.ExcludeTypeFromAutoRegistration(typeof(SimulationUnit));
            });
            
            Current = container;
        }

        public static DependencyInjectionContainer Current { get; }
    }
}