using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using Reflight.Core;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui.ViewModels
{


    public class SettingsViewModel : ReactiveObject, IDisposable, ISettings
    {
        private readonly ObservableAsPropertyHelper<ISimulationViewModel> dashboardPreview;
        private readonly IDisposable disposable;
        private readonly ReadOnlyObservableCollection<FolderViewModel> folders;
        private readonly ISettingsStore settingsStore;
        private readonly IVirtualDashboardRepository virtualDashboardRepository;
        private UnitPack unitPack;

        public SettingsViewModel(IAccesibleFolders accesibleFolders, ISettingsStore settingsStore,
            IVirtualDashboardRepository virtualDashboardRepository)
        {
            this.settingsStore = settingsStore;
            this.virtualDashboardRepository = virtualDashboardRepository;
            AddFolder = ReactiveCommand.CreateFromTask(accesibleFolders.Add);

            disposable = accesibleFolders.Folders
                .Connect()
                .Transform(x => new FolderViewModel(x, accesibleFolders))
                .Bind(out folders)
                .Subscribe();

            UnitPack = UnitPacks.FirstOrDefault(pack => pack.Id == StringUnitPack) ?? UnitPacks.First();
            this.WhenAnyValue(x => x.UnitPack).Subscribe(x => StringUnitPack = UnitPack.Id);

            dashboardPreview =
                this.WhenAnyValue(x => x.VirtualDashboard, x => x.UnitPack,
                    (vd, up) => SettingsSampleData.CreatePreviewViewModel(up, vd)).ToProperty(this, x => x.DashboardPreview);
        }

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

        public ISimulationViewModel DashboardPreview => dashboardPreview.Value;

        public ReactiveCommand<Unit, Folder> AddFolder { get; }

        public ReadOnlyObservableCollection<FolderViewModel> Folders => folders;

        public ICollection<UnitPack> UnitPacks => UnitSource.UnitPacks;

        public UnitPack UnitPack
        {
            get => unitPack;
            set => this.RaiseAndSetIfChanged(ref unitPack, value);
        }

        public string StringUnitPack
        {
            get => settingsStore.Get<string>();
            set => settingsStore.Set(value);
        }

        public VirtualDashboard VirtualDashboard
        {
            get
            {
                var name = settingsStore.Get<string>();
                return VirtualDashboards.FirstOrDefault(x => x.Name == name) ?? VirtualDashboards.First();
            }
            set
            {
                settingsStore.Set(value.Name);
                this.RaisePropertyChanged();
            }
        }

        public IEnumerable<VirtualDashboard> VirtualDashboards => virtualDashboardRepository.GetAll();

        public void Dispose()
        {
            AddFolder?.Dispose();
            disposable?.Dispose();
        }
    }
}