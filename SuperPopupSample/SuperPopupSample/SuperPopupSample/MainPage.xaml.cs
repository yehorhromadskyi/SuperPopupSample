using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public partial class MainPage : ContentPage
    {
        public ICommand ShowPopup1Command { get; set; }

        public ICommand ShowPopup2Command { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ShowPopup1Command = new Command(ExecuteShowPopup1Command);
            ShowPopup2Command = new Command(ExecuteShowPopup2Command);

            var vm = new MainPageViewModel();
            BindingContext = vm;

            PopupService.Register(PopupType.Popup1, PopupView1);
            PopupService.Register(PopupType.Popup2, PopupView2);
        }

        private void ExecuteShowPopup1Command(object obj)
        {
            System.Diagnostics.Debug.WriteLine("MainPage ShowCommand");

            if (obj is Point location)
            {
                PopupView1.Location = location;
            }

            (BindingContext as MainPageViewModel).ShowPopup1Command.Execute(null);
        }

        private void ExecuteShowPopup2Command(object obj)
        {
            if (obj is Point location)
            {
                PopupView2.Location = location;
            }

            (BindingContext as MainPageViewModel).ShowPopup2Command.Execute(null);
        }
    }
}
