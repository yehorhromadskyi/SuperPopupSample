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
        View _view;

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
            _view = view;

            _rootLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ClickOutsidePopupContent)
            });

            _rootLayout.Children.Add(view);

            Content = _rootLayout;
        }

        async void ClickOutsidePopupContent()
        {
            await HideAsync();
        }

        public async Task ShowAsync(Point location)
        {
            AbsoluteLayout.SetLayoutFlags(_view, AbsoluteLayoutFlags.SizeProportional);
            AbsoluteLayout.SetLayoutBounds(_view, new Rectangle(location.X, location.Y, 0.5, 0.5));

            PopupContent.Scale = 0;
            IsVisible = true;
            await PopupContent.ScaleTo(1, 100, Easing.CubicOut);
        }

        public async Task HideAsync()
        {
            await PopupContent.ScaleTo(0, 100, Easing.CubicOut);
            IsVisible = false;
            PopupContent.Scale = 1;
        }

        void OnIsOpenedChanged(bool isOpen)
        {
            if (isOpen)
            {
                
            }
            else
            {
                
            }
        }
    }
}
