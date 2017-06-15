using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public class Popup : ContentView
    {
        public Popup()
        {
        }

        public void Show()
        {
            var manager = DependencyService.Get<IPopupManager>();
            manager.Show(this);
        }

        public void Hide()
        {
            var manager = DependencyService.Get<IPopupManager>();
            manager.Hide(this);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.Windows)
            {
                Layout(DependencyService.Get<IScreenManager>().ScreenSize);
            }
        }
    }
}
