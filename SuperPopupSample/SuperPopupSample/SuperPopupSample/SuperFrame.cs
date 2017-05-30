using System;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public class ArrowOptions
    {
        public Point Location { get; set; }
        public double RotationAngle { get; set; }
        public Color Color { get; set; }
        public double Size { get; set; }
    }

    public class SuperFrame : Frame
    {
        public event EventHandler<ArrowOptions> DrawArrowRequested;

        public void DrawArrow(ArrowOptions request)
        {
            DrawArrowRequested?.Invoke(this, request);
        }
    }
}
