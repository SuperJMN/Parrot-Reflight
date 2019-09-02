using System.Threading.Tasks;
using DynamicData;

namespace Reflight.Core
{
    public interface IAccesibleFolders
    {
        Task<Folder> Add();
        Task Delete(Folder folder);
        IObservableCache<Folder, string> Folders { get; }
    }
}