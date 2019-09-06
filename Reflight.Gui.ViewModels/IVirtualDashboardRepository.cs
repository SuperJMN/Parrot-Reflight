using System.Collections.Generic;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui.ViewModels
{
    public interface IVirtualDashboardRepository
    {
        ICollection<VirtualDashboard> GetAll();
    }
}