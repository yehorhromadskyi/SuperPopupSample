using SuperPopupSample.UWP;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Xamarin.Forms;

[assembly:Dependency(typeof(ScreenManagerImplementation))]
namespace SuperPopupSample.UWP
{
    public class ScreenManagerImplementation : IScreenManager
    {
        public Xamarin.Forms.Rectangle ScreenSize
        {
            get
            {
                var windowBound = Window.Current.Bounds;
                var inputPane = InputPane.GetForCurrentView();
                var keyboardBounds = inputPane.OccludedRect;

                var bound = new Rect
                {
                    Height = windowBound.Height - keyboardBounds.Height,
                    Width = windowBound.Width,
                };

                return new Xamarin.Forms.Rectangle(0, 0, bound.Width, bound.Height);
            }
        }
    }
}
