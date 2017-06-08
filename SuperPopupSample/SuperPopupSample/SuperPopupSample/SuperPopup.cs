using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperPopupSample
{
    [ContentProperty(nameof(PopupContent))]
    public class SuperPopup : ContentView
    {
        const double ContentMargin = 10;

        AbsoluteLayout _rootLayout;
        SuperFrame _contentFrame;

        double yOffset;

        public static readonly BindableProperty PopupContentProperty =
            BindableProperty.Create(nameof(PopupContent),
                                    typeof(View),
                                    typeof(SuperPopup),
                                    default(View),
                                    propertyChanged: OnPopupContentPropertyChanged);

        public static readonly BindableProperty PlacementProperty =
            BindableProperty.Create(nameof(Placement),
                                    typeof(Placement),
                                    typeof(SuperPopup),
                                    Placement.LocationRequest);

        public static readonly BindableProperty LocationRequestProperty =
            BindableProperty.Create(nameof(LocationRequest),
                                    typeof(Point),
                                    typeof(SuperPopup),
                                    new Point(0, 0));

        public static readonly BindableProperty ProportionalSizeProperty =
            BindableProperty.Create(nameof(ProportionalSize),
                                    typeof(Size),
                                    typeof(SuperPopup),
                                    default(Size));

        public static readonly BindableProperty RequiredSizeProperty =
            BindableProperty.Create(nameof(RequiredSize),
                                    typeof(Size),
                                    typeof(SuperPopup),
                                    default(Size));

        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(nameof(IsOpen),
                                    typeof(bool),
                                    typeof(SuperPopup),
                                    false,
                                    propertyChanged: OnIsOpenPropertyChanged);

        public static readonly BindableProperty IsArrowVisibleProperty =
            BindableProperty.Create(nameof(IsArrowVisible),
                                    typeof(bool),
                                    typeof(SuperPopup),
                                    false);


        public static readonly BindableProperty AnimateProperty =
            BindableProperty.Create(nameof(Animate),
                                    typeof(bool),
                                    typeof(SuperPopup),
                                    true);

        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public Placement Placement
        {
            get { return (Placement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public Point LocationRequest
        {
            get { return (Point)GetValue(LocationRequestProperty); }
            set { SetValue(LocationRequestProperty, value); }
        }

        [TypeConverter(typeof(SizeTypeConverter))]
        public Size ProportionalSize
        {
            get { return (Size)GetValue(ProportionalSizeProperty); }
            set { SetValue(ProportionalSizeProperty, value); }
        }

        [TypeConverter(typeof(SizeTypeConverter))]
        public Size RequiredSize
        {
            get { return (Size)GetValue(RequiredSizeProperty); }
            set { SetValue(RequiredSizeProperty, value); }
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public bool IsArrowVisible
        {
            get { return (bool)GetValue(IsArrowVisibleProperty); }
            set { SetValue(IsArrowVisibleProperty, value); }
        }

        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }

        /// <summary>
        /// Coordinates of the Top-Left corner of PopupContent.
        /// </summary>
        public Point Location { get; private set; }

        public SuperPopup()
        {
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (_rootLayout != null)
            {
                var y = _rootLayout.Y;
                var parent = (VisualElement)_rootLayout.Parent;
                while (parent != null)
                {
                    y += parent.Y;
                    parent = parent.Parent as VisualElement;
                }

                yOffset = y;
            }
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
            // Note that UWP will not handle tap on AbsoluteLayout if remove Background
            _rootLayout = new AbsoluteLayout() { BackgroundColor = Color.Transparent };

            _contentFrame = new SuperFrame
            {
                Padding = new Thickness(0),
                HasShadow = false,
                Content = view
            };

            _rootLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ClickOutsidePopupContent)
            });

            _contentFrame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ClickOnPopupContent)
            });

            UpdateRequiredSize(RequiredSize);
            UpdateProportionalSize(ProportionalSize);

            _rootLayout.Children.Add(_contentFrame);
            Content = _rootLayout;

            Opacity = 0;
            InputTransparent = true;
            _rootLayout.Opacity = 0;
            _rootLayout.InputTransparent = true;
        }

        void ClickOnPopupContent()
        {
            // Ignoring required for iOS to prevent ClickOutsidePopupContent from calling.
        }

        void ClickOutsidePopupContent()
        {
            IsOpen = false;
        }

        void UpdateLocation(Point location)
        {
            if (_contentFrame != null)
            {
                var arrowOptions = new ArrowOptions();
                var x = location.X;
                var y = location.Y - yOffset;
                var width = _contentFrame.Width;
                var height = _contentFrame.Height;

                CalculateLocation(arrowOptions, ref x, ref y, width, height);

                arrowOptions.Location = new Point
                {
                    X = Math.Min(Math.Max(location.X - x, ContentMargin * 1.5d), width - ContentMargin * 1.5d),
                    Y = location.Y - y - yOffset
                };

                if (!ProportionalSize.IsZero)
                {
                    width = ProportionalSize.Width;
                    height = ProportionalSize.Height;
                    AbsoluteLayout.SetLayoutFlags(_contentFrame, AbsoluteLayoutFlags.SizeProportional);
                }

                Location = new Point(x, y);

                AbsoluteLayout.SetLayoutBounds(_contentFrame, new Rectangle(x, y, width, height));

                if (IsArrowVisible)
                {
                    _contentFrame.DrawArrow(arrowOptions);
                }
            }
        }

        private void CalculateLocation(ArrowOptions arrowOptions, ref double x, ref double y, double width, double height)
        {
            switch (Placement)
            {
                case Placement.LocationRequest:
                    // crossed the right edge of the screen
                    if (x + width / 2 + ContentMargin > _rootLayout.Width)
                    {
                        x = _rootLayout.Width - width - ContentMargin;
                    }
                    else
                    {
                        x = Math.Max(x - width / 2, ContentMargin);
                    }

                    // crossed the bottom edge of the screen
                    if (y + height + ContentMargin * 2 > _rootLayout.Height)
                    {
                        if (y - height - ContentMargin >= 0)
                        {
                            y = y - height - ContentMargin;
                            arrowOptions.Direction = ArrowDirection.Down;
                        }
                        else
                        {
                            y += ContentMargin;
                        }
                    }
                    else
                    {
                        y += ContentMargin;
                    }

                    break;

                case Placement.PageCenter:
                    if (width >= _rootLayout.Width)
                    {
                        x = 0;
                    }

                    if (height >= _rootLayout.Height)
                    {
                        y = 0;
                    }

                    x = (_rootLayout.Width - width) / 2;
                    y = (_rootLayout.Height - height) / 2;

                    break;
            }
        }

        void UpdateProportionalSize(Size size)
        {
            if (_contentFrame != null && !size.IsZero)
            {
                AbsoluteLayout.SetLayoutFlags(_contentFrame, AbsoluteLayoutFlags.SizeProportional);
                AbsoluteLayout.SetLayoutBounds(_contentFrame, new Rectangle(0, 0, size.Width, size.Height));
            }
        }

        void UpdateRequiredSize(Size size)
        {
            if (_contentFrame != null && !size.IsZero)
            {
                AbsoluteLayout.SetLayoutFlags(_contentFrame, AbsoluteLayoutFlags.None);
                AbsoluteLayout.SetLayoutBounds(_contentFrame, new Rectangle(0, 0, size.Width, size.Height));
            }
        }

        static void OnIsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var popup = bindable as SuperPopup;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await popup.UpdateIsOpen((bool)newValue);
            });
        }

        Task UpdateIsOpen(bool isOpen)
        {
            if (isOpen)
            {
                UpdateLocation(LocationRequest);
                return ShowAsync();
            }
            else
            {
                return HideAsync();
            }
        }

        async Task ShowAsync()
        {
            if (Animate)
            {
                _contentFrame.Scale = 0;
            }

            Opacity = 1;
            InputTransparent = false;
            _rootLayout.Opacity = 1;
            _rootLayout.InputTransparent = false;

            if (Animate)
            {
                await _contentFrame.ScaleTo(1, 100);
            }
        }

        async Task HideAsync()
        {
            if (Animate)
            {
                await _contentFrame.ScaleTo(0, 100);
            }

            Opacity = 0;
            InputTransparent = true;
            _rootLayout.Opacity = 0;
            _rootLayout.InputTransparent = true;

            if (Animate)
            {
                _contentFrame.Scale = 1;
            }
        }
    }
}
