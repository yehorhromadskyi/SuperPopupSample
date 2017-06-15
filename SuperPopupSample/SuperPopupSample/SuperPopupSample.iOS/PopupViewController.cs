using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using XFPlatform = Xamarin.Forms.Platform.iOS.Platform;
using Xamarin.Forms;

namespace SuperPopupSample.iOS
{
    [Register("PopupViewController")]
    public class PopupViewController : UIViewController
    {
        Popup popup;

        public PopupViewController(Popup popup)
        {
            this.popup = popup;
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            var renderer = XFPlatform.GetRenderer(popup);
            if (renderer == null)
            {
                renderer = XFPlatform.CreateRenderer(popup);
                XFPlatform.SetRenderer(popup, renderer);
            }

            popup.Layout(DependencyService.Get<IScreenManager>().ScreenSize);

            View = renderer.NativeView;

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }
    }
}