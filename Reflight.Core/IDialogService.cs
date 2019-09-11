using System.Threading.Tasks;

namespace Reflight.Core
{
    public interface IDialogService
    {
        Task ShowError(string title, string message);
        Task ShowMessage(string message);
    }
}