using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperPopupSample
{
    [ContentProperty(nameof(PopupContent))]
    public class SuperPopup : ContentView, IPopup
    {
        AbsoluteLayout _rootLayout;
        View _view;

        public static readonly BindableProperty PopupContentProperty =
            BindableProperty.Create(nameof(PopupContent),
                                    typeof(View),
                                    typeof(SuperPopup),
                                    default(View),
                                    propertyChanged: OnPopupContentPropertyChanged);

        public static readonly BindableProperty LocationProperty =
            BindableProperty.Create(nameof(Location),
                                    typeof(Point),
                                    typeof(SuperPopup),
                                    default(Point),
                                    propertyChanged: OnLocationPropertyChanged);

        public static readonly BindableProperty ProportionalSizeProperty =
            BindableProperty.Create(nameof(ProportionalSize),
                                    typeof(Size),
                                    typeof(SuperPopup),
                                    default(Size),
                                    propertyChanged: OnProportionalSizePropertyChanged);

        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public Point Location
        {
            get { return (Point)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        [TypeConverter(typeof(SizeTypeConverter))]
        public Size ProportionalSize
        {
            get { return (Size)GetValue(ProportionalSizeProperty); }
            set { SetValue(ProportionalSizeProperty, value); }
        }

        public SuperPopup()
        {
        }

        public async Task ShowAsync()
        {
            PopupContent.Scale = 0;
            IsVisible = true;
            await PopupContent.ScaleTo(1, 100);
        }

        public async Task HideAsync()
        {
            await PopupContent.ScaleTo(0, 100);
            IsVisible = false;
            PopupContent.Scale = 1;
        }

        static void OnPopupContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is View view)
            {
                var popup = bindable as SuperPopup;
                popup.UpdateContent(view);
            }
        }

        void UpdateContent(View view)
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

            UpdateProportionalSize(ProportionalSize);
            UpdateLocation(Location);

            _rootLayout.Children.Add(view);
            Content = _rootLayout;
        }

        void ClickOnPopupContent()
        {
            // Ignoring required for iOS to prevent ClickOutsidePopupContent from calling.
        }

        async void ClickOutsidePopupContent()
        {
            await HideAsync();
        }

        static void OnLocationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is Point location)
            {
                var popup = bindable as SuperPopup;
                popup.UpdateLocation(location);
            }
        }

        void UpdateLocation(Point location)
        {
            if (_view != null)
            {
                var x = location.X;
                var y = location.Y;
                var width = _view.Width;
                var height = _view.Height;

                if (x + width > _rootLayout.Width)
                    x = x - width;

                if (y + height > _rootLayout.Height)
                    y = y - height;

                // Check if AbsoluteLayout should set proportional size to content.
                if ((AbsoluteLayout.GetLayoutFlags(_view) &
                        AbsoluteLayoutFlags.SizeProportional) == AbsoluteLayoutFlags.SizeProportional)
                {
                    width = ProportionalSize.Width;
                    height = ProportionalSize.Height;
                }

                AbsoluteLayout.SetLayoutBounds(_view, new Rectangle(x, y, width, height));
            }
        }

        static void OnProportionalSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is Size size)
            {
                var popup = bindable as SuperPopup;
                popup.UpdateProportionalSize(size);
            }
        }

        void UpdateProportionalSize(Size size)
        {
            if (_view != null && !size.IsZero)
            {
                AbsoluteLayout.SetLayoutFlags(_view, AbsoluteLayoutFlags.SizeProportional);
                AbsoluteLayout.SetLayoutBounds(_view, new Rectangle(Location.X, Location.Y, size.Width, size.Height));
            }
        }
    }
}
