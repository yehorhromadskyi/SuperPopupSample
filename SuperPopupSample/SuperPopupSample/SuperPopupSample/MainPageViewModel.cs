using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public sealed class MainPageViewModel : BindableObject
    {
        public ICommand ShowPopup1Command { get; set; }

        public ICommand ShowPopup2Command { get; set; }

        public MainPageViewModel()
        {
            ShowPopup1Command = new Command(ExecuteShowPopup1Command);
            ShowPopup2Command = new Command(ExecuteShowPopup2Command);
        }
        
        private async void ExecuteShowPopup1Command()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel ShowCommand");

            var popup = PopupService.Resolve(PopupType.Popup1);
            if (popup != null)
            {
                await popup.ShowAsync();
            }
        }

        private async void ExecuteShowPopup2Command()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel ShowCommand 2");

            var popup = PopupService.Resolve(PopupType.Popup2);
            if (popup != null)
            {
                await popup.ShowAsync();
            }
        }
    }
}
