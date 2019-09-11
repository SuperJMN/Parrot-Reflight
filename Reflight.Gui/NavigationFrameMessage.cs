using Windows.UI.Xaml.Controls;

namespace Reflight.Gui
{
    internal class NavigationFrameMessage
    {
        public Frame MainFrame { get; }
        public Frame InnerFrame { get; }

        public NavigationFrameMessage(Frame mainFrame, Frame innerFrame)
        {
            MainFrame = mainFrame;
            InnerFrame = innerFrame;
        }
    }
}