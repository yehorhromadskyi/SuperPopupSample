using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public sealed class MainPageViewModel : BindableObject
    {
        public ICommand TapCommand { get; set; }

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
            TapCommand = new Command(ExecuteTapCommand);
        }

        private void ExecuteTapCommand(object obj)
        {
            IsPopupOpen = true;

            //if (obj is Point location)
            //{
            //    await PopupView.ShowAsync(location, new Size(240, 120));
            //}
        }
    }
}
