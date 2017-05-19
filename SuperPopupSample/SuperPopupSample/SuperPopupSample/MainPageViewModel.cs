using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public sealed class MainPageViewModel : BindableObject
    {
        public ICommand ShowPopupCommand { get; set; }

        public ICommand ShowSizedPopupCommand { get; set; }

        public ICommand ShowProportionallySizedPopupCommand { get; set; }

        public MainPageViewModel()
        {
            ShowPopupCommand = new Command(ExecuteShowPopupCommand);
            ShowSizedPopupCommand = new Command(ExecuteShowSizedPopupCommand);
            ShowProportionallySizedPopupCommand = new Command(ExecuteShowProportionallySizedPopupCommand);
        }

        private async void ExecuteShowPopupCommand(object obj)
        {
            var popup = PopupService.Resolve(PopupType.Popup);
            if (popup != null)
            {
                await popup.ShowAsync();
            }
        }

        private async void ExecuteShowSizedPopupCommand(object obj)
        {
            var popup = PopupService.Resolve(PopupType.SizedPopup);
            if (popup != null)
            {
                await popup.ShowAsync();
            }
        }

        private async void ExecuteShowProportionallySizedPopupCommand(object obj)
        {
            var popup = PopupService.Resolve(PopupType.ProportionallySizedPopup);
            if (popup != null)
            {
                await popup.ShowAsync();
            }
        }
    }
}
