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
        public ICommand ShowPopupCommand { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ShowPopupCommand = new Command(ExecuteShowPopupCommand);

            var vm = new MainPageViewModel();
            BindingContext = vm;

            PopupService.Register(PopupType.Popup, Popup);
            PopupService.Register(PopupType.SizedPopup, SizedPopup);
            PopupService.Register(PopupType.ProportionallySizedPopup, ProportionallySizedPopup);
        }

        protected override void OnTapped(Point point)
        {
            base.OnTapped(point);

            if (!Popup.IsOpen)
            {
                Popup.Location = point;
            }

            if (!SizedPopup.IsOpen)
            {
                SizedPopup.Location = point;
            }

            if (!ProportionallySizedPopup.IsOpen)
            {
                ProportionallySizedPopup.Location = point; 
            }
        }

        private void ExecuteShowPopupCommand(object obj)
        {
            if (obj is Point location)
            {
                Popup.Location = location;
            }

            (BindingContext as MainPageViewModel).ShowPopupCommand.Execute(null);
        }
    }
}
