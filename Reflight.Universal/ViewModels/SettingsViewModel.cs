using Windows.Storage;
using ReactiveUI;
using Reflight.Universal.Core;

namespace Reflight.Universal.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        private readonly SettingsStore settings;

        public SettingsViewModel()
        {
            settings = new SettingsStore(this, ApplicationData.Current.RoamingSettings);
        }

        public string Username
        {
            get => settings.Get<string>();
            set
            {
                settings.Set(value); 
                this.RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => settings.Get<string>();
            set
            {
                settings.Set(value);
                this.RaisePropertyChanged();
            }
        }
    }
}