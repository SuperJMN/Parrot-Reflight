using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Reflight.Gui.ViewModels;
using Reflight.Gui.ViewModels.Dashboards;

namespace Reflight.Gui
{
    internal class VirtualDashboardRepository : IVirtualDashboardRepository
    {
        private readonly ResourceDictionary source;

        public VirtualDashboardRepository(ResourceDictionary source)
        {
            this.source = source;
        }

        public ICollection<VirtualDashboard> GetAll()
        {
            var virtualDashboards = from tuple in source
                where tuple.Value is DataTemplate
                select new VirtualDashboard((string)tuple.Key);

            return virtualDashboards.ToList();
        }        
    }
}