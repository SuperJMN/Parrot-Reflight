using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
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
            BrowseFolderCommand =
                ReactiveCommand.CreateFromObservable(() => Observable.FromAsync(BrowseFolder).Where(x => x != null));
            BrowseFolderCommand.Subscribe(folder => StorageApplicationPermissions.FutureAccessList.Add(folder));
        }

        public ReactiveCommand<Unit, StorageFolder> BrowseFolderCommand { get; }

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

        private async Task<StorageFolder> BrowseFolder()
        {
            var picker = new FolderPicker
            {
                CommitButtonText = "Select",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add("*"); 
            return await picker.PickSingleFolderAsync();
        }
    }
}