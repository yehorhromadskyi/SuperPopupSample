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

        public static readonly BindableProperty TargetProperty =
            BindableProperty.Create(nameof(Target),
                                    typeof(View),
                                    typeof(SuperPopup),
                                    null);

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

        public View Target
        {
            get { return (View)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
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

            var absolutePosition = CalculateAbsolutePosition(_rootLayout);

            yOffset = absolutePosition.Y;
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

        void UpdateLocation()
        {
            if (_contentFrame != null)
            {
                ArrowOptions arrowOptions;
                var width = _contentFrame.Width;
                var height = _contentFrame.Height;

                Location = CalculateLocation(width, height, out arrowOptions);

                if (!ProportionalSize.IsZero)
                {
                    width = ProportionalSize.Width;
                    height = ProportionalSize.Height;
                    AbsoluteLayout.SetLayoutFlags(_contentFrame, AbsoluteLayoutFlags.SizeProportional);
                }

                AbsoluteLayout.SetLayoutBounds(_contentFrame, new Rectangle(Location.X, Location.Y, width, height));

                if (IsArrowVisible)
                {
                    _contentFrame.DrawArrow(arrowOptions);
                }
            }
        }

        Point CalculateLocation(double popupWindowWidth, double popupWindowHeight, out ArrowOptions arrowOptions)
        {
            var x = 0d;
            var y = 0d;
            arrowOptions = new ArrowOptions();

            switch (Placement)
            {
                case Placement.LocationRequest:
                    x = LocationRequest.X;
                    y = LocationRequest.Y - yOffset;

                    // crossed the right edge of the screen
                    if (x + popupWindowWidth / 2 + ContentMargin > _rootLayout.Width)
                    {
                        x = _rootLayout.Width - popupWindowWidth - ContentMargin;
                    }
                    else
                    {
                        x = Math.Max(x - popupWindowWidth / 2, ContentMargin);
                    }

                    // crossed the bottom edge of the screen
                    if (y + popupWindowHeight + ContentMargin * 2 > _rootLayout.Height)
                    {
                        if (y - popupWindowHeight - ContentMargin >= 0)
                        {
                            y = y - popupWindowHeight - ContentMargin;
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

                    arrowOptions.Location = new Point
                    {
                        X = Math.Min(Math.Max(LocationRequest.X - x, ContentMargin * 1.5d), popupWindowWidth - ContentMargin * 1.5d),
                        Y = LocationRequest.Y - y - yOffset
                    };

                    break;

                case Placement.PageCenter:
                    if (popupWindowWidth < _rootLayout.Width)
                    {
                        x = (_rootLayout.Width - popupWindowWidth) / 2;
                    }

                    if (popupWindowHeight < _rootLayout.Height)
                    {
                        y = (_rootLayout.Height - popupWindowHeight) / 2;
                    }

                    arrowOptions.Location = new Point
                    {
                        X = popupWindowWidth / 2,
                        Y = -ContentMargin
                    };

                    break;

                case Placement.BelowTargetAtCenter:
                case Placement.BelowTargetAtLeft:
                case Placement.BelowTargetAtRight:
                    if (Target != null)
                    {
                        var popupPosition = CalculatePositionRelatedToTarget(popupWindowWidth, out arrowOptions);
                        x = popupPosition.X;
                        y = popupPosition.Y;
                    }

                    break;
            }

            return new Point(x, y);
        }

        private Point CalculatePositionRelatedToTarget(double popupWindowWidth, out ArrowOptions arrowOptions)
        {
            var targetPosition = CalculateAbsolutePosition(Target);

            arrowOptions = new ArrowOptions();
            var x = 0d;
            var y = targetPosition.Y + Target.Bounds.Height + ContentMargin;

            switch (Placement)
            {
                case Placement.BelowTargetAtCenter:
                    x = targetPosition.X - (popupWindowWidth / 2) + Target.Bounds.Width / 2;
                    arrowOptions.Location = new Point
                    {
                        X = popupWindowWidth / 2,
                        Y = -ContentMargin
                    };
                    break;

                case Placement.BelowTargetAtLeft:
                    x = targetPosition.X;
                    arrowOptions.Location = new Point
                    {
                        X = ContentMargin * 1.5,
                        Y = -ContentMargin
                    };
                    break;

                case Placement.BelowTargetAtRight:
                    x = targetPosition.X - popupWindowWidth + Target.Bounds.Width;
                    arrowOptions.Location = new Point
                    {
                        X = popupWindowWidth - ContentMargin * 1.5,
                        Y = -ContentMargin
                    };
                    break;
            }

            return new Point(x, y);
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
                UpdateLocation();
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

        Point CalculateAbsolutePosition(VisualElement view)
        {
            if (view == null)
            {
                return Point.Zero;
            }

            var x = view.X;
            var y = view.Y;
            var parent = view.Parent as VisualElement;
            while (parent != null)
            {
                x += parent.X;
                y += parent.Y;
                parent = parent.Parent as VisualElement;
            }

            return new Point(x, y);
        }
    }
}
