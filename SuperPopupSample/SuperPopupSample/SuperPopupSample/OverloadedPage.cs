using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace SuperPopupSample
{
    public class OverloadedPage : ContentPage
    {
        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                OnPropertyChanged();
            }
        }

        public OverloadedPage()
        {
            BindingContext = this;

            var rootGrid = new Grid();
            var scroll = new ScrollView();
            var stack = new StackLayout
            {
                BackgroundColor = Color.Green
            };

            for (int i = 0; i < 100; i++)
            {
                var label = new Label
                {
                    Text = $"Label {i}"
                };

                var button = new Button
                {
                    Text = $"Button {i}",
                    Command = new Command(() =>
                    {
                        IsOpen = true;
                    })
                };

                stack.Children.Add(label);
                stack.Children.Add(button);
            }

            scroll.Content = stack;

            rootGrid.Children.Add(scroll);

            for (int i = 0; i < 1000; i++)
            {
                var popupStack = new StackLayout
                {
                    BackgroundColor = Color.Red
                };

                var popupLabel = new Label
                {
                    Text = $"Popup Label {i}"
                };

                var popupButton = new Button
                {
                    Text = $"Popup Button {i}",
                    Command = new Command(() =>
                    {
                        IsOpen = false;
                    })
                };

                popupStack.Children.Add(popupLabel);
                popupStack.Children.Add(popupButton);

                var popup = new SuperPopup
                {
                    IsArrowVisible = false,
                    PopupContent = popupStack
                };

                popup.SetBinding(SuperPopup.IsOpenProperty, new Binding("IsOpen"));

                rootGrid.Children.Add(popup);
            }

            Content = rootGrid;
        }
    }
}