using SuperPopupSample;
using SuperPopupSample.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SuperFrame), typeof(SuperFrameRenderer))]
namespace SuperPopupSample.UWP
{
    public class SuperFrameRenderer : ViewRenderer<ContentView, Windows.UI.Xaml.Controls.ContentControl>
    {
        SuperFrame _superFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            if (e.OldElement != null)
            {
                _superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                _superFrame = null;
            }

            if (e.NewElement != null)
            {
                _superFrame = e.NewElement as SuperFrame;

                _superFrame.DrawArrowRequested += OnDrawArrowRequest;

                if (Control == null)
                {
                    var contentControl = new Windows.UI.Xaml.Controls.ContentControl();

                    var style = App.Current.Resources["ArrowedContentViewStyle"] as Windows.UI.Xaml.Style;
                    if (style != null)
                    {
                        contentControl.Style = style;
                    }

                    SetNativeControl(contentControl);
                }
            }

            base.OnElementChanged(e);
        }

        private void OnDrawArrowRequest(object sender, ArrowOptions e)
        {
            DrawArrow(e);
        }

        private void DrawArrow(ArrowOptions options)
        {
            for (int i = 0; i < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(Control); i++)
            {
                var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(Control, i);
                for (int j = 0; j < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(child); j++)
                {
                    var gridChild = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(child, j);
                    if (gridChild is Windows.UI.Xaml.Shapes.Path path)
                    {
                        var rotateTransform = path.RenderTransform as Windows.UI.Xaml.Media.CompositeTransform;
                        rotateTransform.Rotation = options.RotationAngle - 90;
                        //rotateTransform.TranslateX = options.Location.X;
                        //rotateTransform.TranslateY = options.Location.Y;
                        var y = options.Location.Y - _superFrame.Height / 2;
                        var x = options.Location.X - _superFrame.Width / 2;
                        System.Diagnostics.Debug.WriteLine($"---------------X: {x}");
                        rotateTransform.TranslateX = x;
                        rotateTransform.TranslateY = y;
                    }
                }
            }
        }
    }
}
