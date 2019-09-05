using System.Reactive;
using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface IMetadataProperties
    {
        string Duration { get; }
        string DateEncoded { get; }
    }

    public interface INavigation
    {
        Task Go<T>(object parameter = null);
        Task GoBack();
    }
}