using System;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public enum ArrowDirection
    {
        Up,
        Down
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public sealed class SuperFrame : Frame
    {
        public event EventHandler DrawArrowRequested;

        public static readonly BindableProperty ArrowColorProperty =
            BindableProperty.Create(nameof(ArrowColor),
                                    typeof(Color),
                                    typeof(SuperFrame),
                                    Color.White);

        public static readonly BindableProperty HorizontalArrowAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalArrowAlignment),
                                    typeof(HorizontalAlignment),
                                    typeof(SuperFrame),
                                    default(HorizontalAlignment));

        public static readonly BindableProperty ArrowDirectionProperty =
            BindableProperty.Create(nameof(ArrowDirection),
                                    typeof(ArrowDirection),
                                    typeof(SuperFrame),
                                    default(ArrowDirection));

        public Color ArrowColor
        {
            get { return (Color)GetValue(ArrowColorProperty); }
            set { SetValue(ArrowColorProperty, value); }
        }

        public HorizontalAlignment HorizontalArrowAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalArrowAlignmentProperty); }
            set { SetValue(HorizontalArrowAlignmentProperty, value); }
        }

        public ArrowDirection ArrowDirection
        {
            get { return (ArrowDirection)GetValue(ArrowDirectionProperty); }
            set { SetValue(ArrowDirectionProperty, value); }
        }

        public void DrawArrow()
        {
            DrawArrowRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
