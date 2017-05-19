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
        public ICommand ShowPopupCommand { get; set; }

        public ICommand ShowSizedPopupCommand { get; set; }

        public ICommand ShowProportionallySizedPopupCommand { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ShowPopupCommand = new Command(ExecuteShowPopupCommand);
            ShowSizedPopupCommand = new Command(ExecuteShowSizedPopupCommand);
            ShowProportionallySizedPopupCommand = new Command(ExecuteShowProportionallySizedPopupCommand);

            var vm = new MainPageViewModel();
            BindingContext = vm;

            PopupService.Register(PopupType.Popup, Popup);
            PopupService.Register(PopupType.SizedPopup, SizedPopup);
            PopupService.Register(PopupType.ProportionallySizedPopup, ProportionallySizedPopup);
        }

        private void ExecuteShowPopupCommand(object obj)
        {
            if (obj is Point location)
            {
                Popup.Location = location;
            }

            (BindingContext as MainPageViewModel).ShowPopupCommand.Execute(null);
        }

        private void ExecuteShowSizedPopupCommand(object obj)
        {
            if (obj is Point location)
            {
                SizedPopup.Location = location;
            }

            (BindingContext as MainPageViewModel).ShowSizedPopupCommand.Execute(null);
        }

        private void ExecuteShowProportionallySizedPopupCommand(object obj)
        {
            if (obj is Point location)
            {
                ProportionallySizedPopup.Location = location;
            }

            (BindingContext as MainPageViewModel).ShowProportionallySizedPopupCommand.Execute(null);
        }
    }
}
