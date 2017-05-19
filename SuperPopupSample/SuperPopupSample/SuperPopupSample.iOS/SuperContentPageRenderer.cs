using Foundation;
using SuperPopupSample;
using SuperPopupSample.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SuperContentPage), typeof(SuperContentPageRenderer))]
namespace SuperPopupSample.iOS
{
    public class SuperContentPageRenderer : PageRenderer
    {
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (Element is SuperContentPage page)
            {
                foreach (var touch in touches.ToArray<UITouch>())
                {
                    var cgPoint = touch.LocationInView(View);

                    page.InvokeTapped(new Point(cgPoint.X, cgPoint.Y));
                }
            }
        }
    }
}