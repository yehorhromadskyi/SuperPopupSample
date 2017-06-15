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
using XFPlatform = Xamarin.Forms.Platform.Android.Platform;
using SuperPopupSample.Droid;

[assembly: Dependency(typeof(PopupManagerImplementation))]
namespace SuperPopupSample.Droid
{
    public class PopupManagerImplementation : IPopupManager
    {
        public void Show(Popup popup)
        {
            var decoreView = (FrameLayout)((Activity)Forms.Context).Window.DecorView; ;

            var renderer = XFPlatform.GetRenderer(popup);
            if (renderer == null)
            {
                renderer = XFPlatform.CreateRenderer(popup);
                XFPlatform.SetRenderer(popup, renderer);
            }

            popup.Layout(DependencyService.Get<IScreenManager>().ScreenSize);

            decoreView.AddView(renderer.ViewGroup);
        }

        public void Hide(Popup popup)
        {
            var decoreView = (FrameLayout)((Activity)Forms.Context).Window.DecorView; ;

            var renderer = XFPlatform.GetRenderer(popup);
            if (renderer == null)
            {
                renderer = XFPlatform.CreateRenderer(popup);
                XFPlatform.SetRenderer(popup, renderer);
            }

            if (renderer != null)
            {
                decoreView.RemoveView(renderer.ViewGroup);
            }
        }
    }
}