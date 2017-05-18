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
        UIView _view;
        UITapGestureRecognizer _tapRecognizer;

        protected override void OnAttached()
        {
            _view = Control ?? Container;
            _view.UserInteractionEnabled = true;

            _tapRecognizer = new UITapGestureRecognizer(recognizer =>
            {
                var command = Gestures.GetTappedCommand(Element);
                if (command != null)
                {
                    Point point;
                    var screenSize = UIScreen.MainScreen.Bounds;

                    // Passing null to detect absolute location on UIScreen
                    var cgPoint = recognizer.LocationInView(null);

                    switch (UIApplication.SharedApplication.StatusBarOrientation)
                    {
                        default:
                        case UIInterfaceOrientation.Portrait:
                            point = new Point(cgPoint.X, cgPoint.Y);
                            break;
                        case UIInterfaceOrientation.PortraitUpsideDown:
                            point = new Point(screenSize.Width - cgPoint.X, screenSize.Height - cgPoint.Y);
                            break;
                        case UIInterfaceOrientation.LandscapeLeft:
                            point = new Point(screenSize.Width - cgPoint.Y, cgPoint.X);
                            break;
                        case UIInterfaceOrientation.LandscapeRight:
                            point = new Point(cgPoint.Y, screenSize.Height - cgPoint.X);
                            break;
                    }

                    if (command.CanExecute(point))
                    {
                        command.Execute(point);
                    }
                }
            });

            _view.AddGestureRecognizer(_tapRecognizer);
        }

        protected override void OnDetached()
        {
            _view.RemoveGestureRecognizer(_tapRecognizer);
            _view = null;
            _tapRecognizer = null;
        }
    }
}