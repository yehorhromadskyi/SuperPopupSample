using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperPopupSample
{
    [ContentProperty(nameof(PopupContent))]
    public class SuperPopup : ContentView
    {
        AbsoluteLayout _rootLayout;

        public static readonly BindableProperty PopupContentProperty =
            BindableProperty.Create(nameof(PopupContent),
                                    typeof(View),
                                    typeof(SuperPopup),
                                    default(View),
                                    propertyChanged: OnPopupContentPropertyChanged);

        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }


        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(nameof(IsOpen),
                                    typeof(bool),
                                    typeof(SuperPopup),
                                    default(bool),
                                    propertyChanged: (view, _, isOpen) =>
                                    {
                                        ((SuperPopup)view).OnIsOpenedChanged((bool)isOpen);
                                    });

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public SuperPopup()
        {
        }

        private static void OnPopupContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is View view)
            {
                var popup = bindable as SuperPopup;
                popup.Update(view);
            }
        }

        void Update(View view)
        {
            IsVisible = false;
            _rootLayout = new AbsoluteLayout();

            _rootLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ClickOutsidePopupContent)
            });

            AbsoluteLayout.SetLayoutFlags(view, AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.XProportional);
            AbsoluteLayout.SetLayoutBounds(view, new Rectangle(1, 100, 0.5, 0.5));

            _rootLayout.Children.Add(view);

            Content = _rootLayout;
        }

        void ClickOutsidePopupContent()
        {
            IsOpen = false;
        }

        async void OnIsOpenedChanged(bool isOpen)
        {
            if (isOpen)
            {
                IsVisible = true;
                PopupContent.Scale = 0.05;
                await PopupContent.ScaleTo(1, 100, Easing.CubicOut);
            }
            else
            {
                await PopupContent.ScaleTo(0.05, 100, Easing.CubicOut);
                IsVisible = false;
            }
        }
    }
}
