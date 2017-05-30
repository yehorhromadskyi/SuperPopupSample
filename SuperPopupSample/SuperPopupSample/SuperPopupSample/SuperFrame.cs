using System;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public class DrawArrowOptions
    {
        public Point Location { get; set; }
        public double Rotation { get; set; }
    }

    public class SuperFrame : Frame
    {
        public event EventHandler<DrawArrowOptions> DrawArrowRequested;

        public static readonly BindableProperty ArrowSizeProperty =
            BindableProperty.Create(nameof(ArrowSize),
                                    typeof(double),
                                    typeof(SuperFrame),
                                    10d);

        public double ArrowSize
        {
            get { return (double)GetValue(ArrowSizeProperty); }
            set { SetValue(ArrowSizeProperty, value); }
        }

        public void DrawArrow(DrawArrowOptions request)
        {
            DrawArrowRequested?.Invoke(this, request);
        }
    }
}
