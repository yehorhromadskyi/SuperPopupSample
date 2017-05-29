using System;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public class DrawArrowRequest
    {
        public Point Location { get; set; }
        public double Rotation { get; set; }
    }

    public class SuperFrame : Frame
    {
        public event EventHandler<DrawArrowRequest> DrawArrowRequested;

        public void DrawArrow(DrawArrowRequest request)
        {
            DrawArrowRequested?.Invoke(this, request);
        }
    }
}
