using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Reflight.Gui.ViewModels;

namespace Reflight.Gui.ViewSupport
{
    public class SimulationViewModelToDataTemplateConverter : DataTemplateSelector
    {
        public ResourceDictionary ResourceDictionary { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (ResourceDictionary != null)
            {
                if (item is ISimulationViewModel vm)
                {
                    var dataTemplate = ResourceDictionary[vm.PresentationOptions.Dashboard.Template] as DataTemplate;
                    return dataTemplate;
                }
            }
            
            return base.SelectTemplateCore(item, container);
        }
    }
}