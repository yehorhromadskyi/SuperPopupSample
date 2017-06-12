using SuperPopupSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(ViewManagerImplementation))]
namespace SuperPopupSample.Droid
{
    public sealed class ViewManagerImplementation : IViewManager
    {
        public Point GetLocationOnScreen(VisualElement view)
        {
            var viewRenderer = Platform.GetRenderer(view);
            if (viewRenderer != null && viewRenderer.ViewGroup != null)
            {
                var location = new int[2];
                viewRenderer.ViewGroup.GetLocationOnScreen(location);
                var convertedLocation = Helpers.PxToDp(new Point(location[0], location[1]), 
                    viewRenderer.ViewGroup.Resources.DisplayMetrics);
                return convertedLocation;
            }

            return Point.Zero;
        }
    }
}
