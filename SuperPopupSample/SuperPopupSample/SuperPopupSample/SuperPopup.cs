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

        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
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

        async void ClickOutsidePopupContent()
        {
            await HideAsync();
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
    }
}
