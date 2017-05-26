using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperPopupSample
{
    [Flags]
    public enum ArrowPlacement
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public class SuperFrame : Frame
    {
        public event EventHandler<ArrowPlacement> DrawArrowRequested;

        public void DrawArrow(ArrowPlacement placement)
        {
            DrawArrowRequested?.Invoke(this, placement);
        }
    }
}
