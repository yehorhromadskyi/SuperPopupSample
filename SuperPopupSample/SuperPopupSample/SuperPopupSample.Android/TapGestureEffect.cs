using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using SuperPopupSample.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Support.V4.View;

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