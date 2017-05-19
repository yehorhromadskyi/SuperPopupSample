using Android.Views;
using SuperPopupSample;
using SuperPopupSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SuperContentPage), typeof(SuperContentPageRenderer))]
namespace SuperPopupSample.Droid
{
    public class SuperContentPageRenderer : PageRenderer
    {
        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (Element is SuperContentPage page)
            {
                if (ev.Action == MotionEventActions.Up)
                {
                    var convertedPoint = Helpers.PxToDp(new Point(ev.GetX(), ev.GetY()),
                        ViewGroup.Resources.DisplayMetrics);

                    page.InvokeTapped(convertedPoint);
                }
            }

            return base.OnInterceptTouchEvent(ev);
        }
    }
}