using System.Reactive;
using ReactiveUI;
using Reflight.Core;

namespace Reflight.Gui.ViewModels
{
    public class FolderViewModel : ReactiveObject
    {
        private readonly Folder folder;

        public FolderViewModel(Folder folder, IAccesibleFolders accesibleFolders)
        {
            this.folder = folder;
            Delete = ReactiveCommand.CreateFromTask(() => accesibleFolders.Delete(folder));
        }

        public ReactiveCommand<Unit, Unit> Delete { get; }

        public string Token => folder.Token;
        public string Path => folder.Path;
    }
}