using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuperPopupSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPopupPage : ContentPage
    {
        public NewPopupPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var frame = new Frame
            {
                BackgroundColor = Color.Green
            };

            var popup = new Popup
            {
                Content = frame
            };

            frame.Content = new Button
            {
                Text = "Hide",
                Command = new Command(() =>
                  {
                      popup.Hide();
                  })
            };

            popup.Show();
        }
    }
}