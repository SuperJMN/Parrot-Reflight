using Windows.UI.Xaml.Controls;

namespace Reflight.Gui
{
    internal class NavigationFrameMessage
    {
        public Frame Frame { get; }

        public NavigationFrameMessage(Frame frame)
        {
            Frame = frame;
        }
    }
}