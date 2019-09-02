using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class SettingsViewModel : ReactiveObject, IDisposable
    {
        private readonly ISettingsStore settingsStore;

        public string Password
        {
            get => settingsStore.Get<string>();
            set => settingsStore.Set(value);
        }

        public string Username
        {
            get => settingsStore.Get<string>();
            set => settingsStore.Set(value);
        }

        private readonly ReadOnlyObservableCollection<FolderViewModel> folders;
        private readonly IDisposable disposable;

        public SettingsViewModel(IAccesibleFolders accesibleFolders, ISettingsStore settingsStore)
        {
            this.settingsStore = settingsStore;
            AddFolder = ReactiveCommand.CreateFromTask(accesibleFolders.Add);
            
            disposable = accesibleFolders.Folders
                .Connect()
                .Transform(x => new FolderViewModel(x, accesibleFolders))
                .Bind(out folders)
                .Subscribe();
        }

        public ReactiveCommand<Unit, Folder> AddFolder { get; }

        public void Dispose()
        {
            AddFolder?.Dispose();
            disposable?.Dispose();
        }

        public ReadOnlyObservableCollection<FolderViewModel> Folders => folders;
    }
}