using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Reflight.Gui.ViewModels;

namespace Reflight.Gui.ViewSupport
{
    public class DroneModelConverter : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is DroneModel dm)
            {
                switch (dm.Name)
                {
                    case "Anafi":
                        return AnafiTemplate;
                    case "Disco":
                        return DiscoTemplate;
                }
            }

            return base.SelectTemplateCore(item, container);
        }

        public DataTemplate DiscoTemplate { get; set; }

        public DataTemplate AnafiTemplate { get; set; }
    }
}