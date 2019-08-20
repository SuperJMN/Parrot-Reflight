using System.Runtime.CompilerServices;

namespace Reflight.Universal.Core
{
    public interface ISettingsStore
    {
        T Get<T>([CallerMemberName] string propertyName = null);
        void Set<T>(T value, [CallerMemberName] string propertyName = null);
    }
}