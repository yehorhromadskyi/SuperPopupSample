using SuperPopupSample.iOS;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFPlatform = Xamarin.Forms.Platform.iOS.Platform;

[assembly: Dependency(typeof(PopupManagerImplementation))]
namespace SuperPopupSample.iOS
{
    public class PopupManagerImplementation : IPopupManager
    {
        UIPopoverController popover;

        public void Show(Popup popup)
        {
            var topViewController = GetTopViewController();

            var popupViewController = new PopupViewController(popup);

            popover = new UIPopoverController(popupViewController);
            popover.PresentFromRect(new CoreGraphics.CGRect(), topViewController.View, UIPopoverArrowDirection.Any, true);
        }

        public void Hide(Popup popup)
        {
            popover.Dismiss(true);
            popover.Dispose();
        }

        private UIViewController GetTopViewController()
        {
            var topViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topViewController.PresentedViewController != null)
            {
                topViewController = topViewController.PresentedViewController;
            }

            return topViewController;
        }
    }
}