using SuperPopupSample.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(ViewManagerImplementation))]
namespace SuperPopupSample.UWP
{
    public sealed class ViewManagerImplementation : IViewManager
    {
        public Point GetLocationOnScreen(VisualElement view)
        {
            var viewRenderer = Platform.GetRenderer(view);
            if (viewRenderer != null && viewRenderer.ContainerElement != null)
            {
                var location = viewRenderer.ContainerElement
                    .TransformToVisual(Windows.UI.Xaml.Window.Current.Content)
                    .TransformPoint(new Windows.Foundation.Point(0, 0));

                return new Point(location.X, location.Y);
            }

            return Point.Zero;
        }
    }
}
