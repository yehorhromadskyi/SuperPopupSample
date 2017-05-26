using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public partial class MainPage : SuperContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        protected override void OnTapped(Point point)
        {
            base.OnTapped(point);

            if (!Popup.IsOpen)
            {
                Popup.LocationRequest = point;
            }
        }
    }
}
