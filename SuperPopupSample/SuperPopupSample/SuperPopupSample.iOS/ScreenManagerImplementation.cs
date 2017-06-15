using SuperPopupSample.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenManagerImplementation))]
namespace SuperPopupSample.iOS
{
    public class ScreenManagerImplementation : IScreenManager
    {
        public Rectangle ScreenSize
        {
            get
            {
                var screen = UIScreen.MainScreen.Bounds;
                var size = new Rectangle
                {
                    Top = screen.Top,
                    Bottom = screen.Bottom,
                    Left = screen.Left,
                    Right = screen.Right,
                    Width = screen.Width,
                    Height = screen.Height
                };

                return size;
            }
        }
    }
}