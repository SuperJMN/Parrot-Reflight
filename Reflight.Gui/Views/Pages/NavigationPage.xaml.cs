﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Extensions;


namespace Reflight.Gui.Views.Pages
{
    public sealed partial class NavigationPage
    {
        public NavigationPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var context = (ValueTuple<object, object>) e.Parameter;
                Content = (UIElement) context.Item1;
                DataContext = context.Item2;
            }
        }
    }
}
