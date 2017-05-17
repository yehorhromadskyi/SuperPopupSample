using SuperPopupSample.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("SuperForms")]
[assembly: ExportEffect(typeof(TapGestureEffect), nameof(TapGestureEffect))]
namespace SuperPopupSample.iOS
{
    public class TapGestureEffect : PlatformEffect
    {
        UITapGestureRecognizer _tapRecognizer;

        protected override void OnAttached()
        {
            _tapRecognizer = new UITapGestureRecognizer(recognizer =>
            {
                var command = Gestures.GetTappedCommand(Element);
                if (command != null)
                {
                    // Passing null to detect absolute location on UIScreen
                    var cgPoint = recognizer.LocationInView(null);
                    var point = new Point(cgPoint.X, cgPoint.Y);

                    if (command.CanExecute(point))
                    {
                        command.Execute(point);
                    }
                }
            });

            Control.AddGestureRecognizer(_tapRecognizer);
        }

        protected override void OnDetached()
        {
            Control.RemoveGestureRecognizer(_tapRecognizer);
            _tapRecognizer = null;
        }
    }
}