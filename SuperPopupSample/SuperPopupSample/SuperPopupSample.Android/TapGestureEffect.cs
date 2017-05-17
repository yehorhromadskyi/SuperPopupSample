using Android.App;
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
                    var statusBarHeight = Helpers.GetStatusBarHeight(_context);

                    int[] coordinates = new int[2];
                    Control.GetLocationOnScreen(coordinates);
                    var x = e.Event.GetX() + coordinates[0];
                    var y = e.Event.GetY() + coordinates[1] - statusBarHeight;

                    var convertedPoint = Helpers.PxToDp(new Point(x, y), _context.Resources.DisplayMetrics);
                    if (command.CanExecute(convertedPoint))
                    {
                        command.Execute(convertedPoint);
                    }
                }
            }
        }
    }
}