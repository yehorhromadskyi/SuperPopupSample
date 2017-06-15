
using Android.App;
using Android.Widget;
using SuperPopupSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ScreenManagerImplementation))]
namespace SuperPopupSample.Droid
{
    public class ScreenManagerImplementation : IScreenManager
    {
        public Rectangle ScreenSize
        {
            get
            {
                var decoreView = (FrameLayout)((Activity)Forms.Context).Window.DecorView;
                return new Rectangle(0.0, 0.0, ContextExtensions.FromPixels(Forms.Context, decoreView.Width),
                    ContextExtensions.FromPixels(Forms.Context, decoreView.Height));
            }
        }
    }
}