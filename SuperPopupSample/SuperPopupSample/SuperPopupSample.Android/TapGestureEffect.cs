using Android.Content;
using Android.Views;
using SuperPopupSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("SuperForms")]
[assembly: ExportEffect(typeof(TapGestureEffect), nameof(TapGestureEffect))]
namespace SuperPopupSample.Droid
{
    public class TapGestureEffect : PlatformEffect
    {
        Context _context;

        protected override void OnAttached()
        {
            _context = Control.Context;
            Control.Touch += OnControlTouch;
        }

        protected override void OnDetached()
        {
            _context = null;
            Control.Touch -= OnControlTouch;
        }

        void OnControlTouch(object sender, Android.Views.View.TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Up)
            {
                var command = Gestures.GetTappedCommand(Element);
                if (command != null)
                {
                    var x = e.Event.GetX();
                    var y = e.Event.GetY();

                    var convertedPoint = PxToDp(new Point(x, y), _context.Resources.DisplayMetrics);
                    if (command.CanExecute(convertedPoint))
                    {
                        command.Execute(convertedPoint);
                    }
                }
            }
        }

        Point PxToDp(Point point, Android.Util.DisplayMetrics displayMetrics)
        {
            point.X = point.X / displayMetrics.Density;
            point.Y = point.Y / displayMetrics.Density;

            return point;
        }
    }
}