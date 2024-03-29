﻿using Windows.UI.Xaml.Controls;
using Reflight.Gui.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Reflight.Gui.Views.Controls
{
    public sealed partial class GeneralSettings : UserControl
    {
        public GeneralSettings()
        {
            this.InitializeComponent();
            this.DataContext = Container.Current.Locate<SettingsViewModel>();
        }
    }
}
