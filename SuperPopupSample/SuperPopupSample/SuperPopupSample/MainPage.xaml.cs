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
        public ICommand TapCommand { get; set; }

        public MainPage()
        {
            InitializeComponent();
            TapCommand = new Command(ExecuteTapCommand);
            BindingContext = this;
        }

        private async void ExecuteTapCommand(object obj)
        {
            if (obj is Point location)
            {
                await PopupView.ShowAsync();
            }
        }
    }
}
