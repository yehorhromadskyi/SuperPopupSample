using SuperPopupSample.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(ViewManagerImplementation))]
namespace SuperPopupSample.iOS
{
    public sealed class ViewManagerImplementation : IViewManager
    {
        public Point GetLocationOnScreen(VisualElement view)
        {
            var viewRenderer = Platform.GetRenderer(view);
            if (viewRenderer != null && viewRenderer.NativeView != null)
            {
                var location = viewRenderer.NativeView.Superview.ConvertPointToView(viewRenderer.NativeView.Frame.Location, null);
                return new Point(location.X, location.Y);
            }

            return Point.Zero;
        }
    }
}
