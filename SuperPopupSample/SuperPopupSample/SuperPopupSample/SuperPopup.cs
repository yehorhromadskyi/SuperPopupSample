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

        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(nameof(IsOpen),
                                    typeof(bool),
                                    typeof(SuperPopup),
                                    default(bool),
                                    propertyChanged: (view, _, isOpen) =>
                                    {
                                        ((SuperPopup)view).OnIsOpenPropertyChanged((bool)isOpen);
                                    });

        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

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

        async void OnIsOpenPropertyChanged(bool isOpen)
        {
            if (isOpen)
            {
                await ShowAsync();
            }
            else
            {
                await HideAsync();
            }
        }

        void Update(View view)
        {
            IsVisible = false;
            // Note that UWP will not handle tap on AbsoluteLayout if remove Background
            _rootLayout = new AbsoluteLayout() { BackgroundColor = Color.Transparent };
            _view = view;

            _rootLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ClickOutsidePopupContent)
            });

            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ClickOnPopupContent)
            });

            _rootLayout.Children.Add(view);

            Content = _rootLayout;
        }

        void ClickOnPopupContent()
        {
            // Ignoring required for iOS to prevent ClickOutsidePopupContent from calling.
        }

        void ClickOutsidePopupContent()
        {
            IsOpen = false;
        }

        public async Task ShowAsync()
        {
            PopupContent.Scale = 0;
            IsVisible = true;
            await PopupContent.ScaleTo(1, 100);
        }

        public Task ShowAsync(Point location)
        {
            AbsoluteLayout.SetLayoutFlags(_view, AbsoluteLayoutFlags.None);
            AbsoluteLayout.SetLayoutBounds(_view, new Rectangle(location.X, location.Y, _view.Width, _view.Height));

            return ShowAsync();
        }

        public Task ShowAsync(Size size)
        {
            AbsoluteLayout.SetLayoutFlags(_view, AbsoluteLayoutFlags.None);
            AbsoluteLayout.SetLayoutBounds(_view, new Rectangle(0, 0, size.Width, size.Height));

            return ShowAsync();
        }

        public Task ShowAsync(Point location, Size size)
        {
            AbsoluteLayout.SetLayoutFlags(_view, AbsoluteLayoutFlags.None);
            AbsoluteLayout.SetLayoutBounds(_view, new Rectangle(location.X, location.Y, size.Width, size.Height));

            return ShowAsync();
        }

        public async Task HideAsync()
        {
            await PopupContent.ScaleTo(0, 100);
            IsVisible = false;
            PopupContent.Scale = 1;
        }
    }
}
