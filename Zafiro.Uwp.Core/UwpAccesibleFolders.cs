using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using DynamicData;
using Reflight.Core;

namespace Zafiro.Uwp.Core
{
    public class UwpAccesibleFolders : IAccesibleFolders
    {
        private readonly SourceCache<Folder, string> source;
        private readonly FolderPicker picker;

        public UwpAccesibleFolders()
        {
            source = new SourceCache<Folder, string>(x => x.Token);
            Folders = source.AsObservableCache();

            var folders = StorageApplicationPermissions.FutureAccessList.Entries.Select(x => new { x.Token, x.Metadata });

            var enumerable = folders.Select(x => new Folder(x.Token, x.Metadata));
            source.AddOrUpdate(enumerable);
            
            picker = new FolderPicker
            {
                CommitButtonText = "Select",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            picker.FileTypeFilter.Add("*");
        }

        public async Task<Folder> Add()
        {
            var file = await picker.PickSingleFolderAsync();

            if (file == null)
            {
                return null;
            }

            var token = StorageApplicationPermissions.FutureAccessList.Add(file, file.Path);
            var folder = new Folder(token, file.Path);
            source.AddOrUpdate(folder);
            return folder;
        }

        public IObservableCache<Folder, string> Folders { get; }

        public Task Delete(Folder folder)
        {
            StorageApplicationPermissions.FutureAccessList.Remove(folder.Token);
            source.Remove(folder.Token);

            return Task.CompletedTask;
        }
    }
}