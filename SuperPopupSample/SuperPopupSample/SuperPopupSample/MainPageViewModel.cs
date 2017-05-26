using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public sealed class MainPageViewModel : BindableObject
    {
        public ICommand ShowPopupCommand { get; private set; }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            ShowPopupCommand = new Command(ExecuteShowPopupCommand);
        }

        private void ExecuteShowPopupCommand(object obj)
        {
            IsPopupOpen = true;
        }
    }
}
