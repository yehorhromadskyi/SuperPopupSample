using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public class SuperContentPage : ContentPage
    {
        public event EventHandler<Point> Tapped;

        public void InvokeTapped(Point point)
        {
            OnTapped(point);
            Tapped?.Invoke(this, point);
        }

        protected virtual void OnTapped(Point point) { }
    }
}
