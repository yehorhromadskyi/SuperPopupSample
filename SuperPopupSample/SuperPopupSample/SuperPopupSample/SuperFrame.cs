using System;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public enum ArrowDirection
    {
        Up,
        Down
    }

    public class ArrowOptions
    {
        public Point Location { get; set; }
        public ArrowDirection Direction { get; set; }
    }

    public sealed class SuperFrame : Frame
    {
        public event EventHandler<ArrowOptions> DrawArrowRequested;

        public static readonly BindableProperty ArrowColorProperty =
            BindableProperty.Create(nameof(ArrowColor),
                                    typeof(Color),
                                    typeof(SuperFrame),
                                    Color.White);

        public Color ArrowColor
        {
            get { return (Color)GetValue(ArrowColorProperty); }
            set { SetValue(ArrowColorProperty, value); }
        }

        public void DrawArrow(ArrowOptions request)
        {
            DrawArrowRequested?.Invoke(this, request);
        }
    }
}
