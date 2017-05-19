using SuperPopupSample;
using SuperPopupSample.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SuperContentPage), typeof(SuperContentPageRenderer))]
namespace SuperPopupSample.UWP
{
    public class SuperContentPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                PointerPressed -= OnPointerPressed;
            }

            if (e.NewElement != null)
            {
                PointerPressed += OnPointerPressed;
            }
        }

        private void OnPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(this);
            if (Element is SuperContentPage page)
            {
                page.InvokeTapped(new Point(pointer.Position.X, pointer.Position.Y));
            }
        }
    }
}