using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Core
{
    public interface ISettings
    {
        string Password { get; set; }
        string Username { get; set; }
        UnitPack UnitPack { get; set; }
        VirtualDashboard VirtualDashboard { get; set; }
        double DashboardScale { get; set; }
    }
}