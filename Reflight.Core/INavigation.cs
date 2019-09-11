using System;
using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface INavigation
    {
        Task Go<T>(object parameter = null);
        Task GoBack();
        IObservable<bool> CanGoBack { get; }
    }
}