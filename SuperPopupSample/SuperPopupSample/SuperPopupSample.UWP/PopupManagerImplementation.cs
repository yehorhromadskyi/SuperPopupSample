using SuperPopupSample.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Controls.Primitives;

[assembly: Dependency(typeof(PopupManagerImplementation))]
namespace SuperPopupSample.UWP
{
    public class PopupManagerImplementation : IPopupManager
    {
        Windows.UI.Xaml.Controls.Primitives.Popup nativePopup;

        public void Show(Popup popup)
        {
            nativePopup = new global::Windows.UI.Xaml.Controls.Primitives.Popup();

            var renderer = popup.GetOrCreateRenderer();
            nativePopup.Child = renderer.ContainerElement;

            nativePopup.IsOpen = true;
            popup.ForceLayout();
        }

        public void Hide(Popup popup)
        {
            nativePopup.IsOpen = false;
            nativePopup.Child = null;
        }
    }
}
