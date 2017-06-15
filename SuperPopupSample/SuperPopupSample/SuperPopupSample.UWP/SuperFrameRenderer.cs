using SuperPopupSample;
using SuperPopupSample.UWP;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SuperFrame), typeof(SuperFrameRenderer))]
namespace SuperPopupSample.UWP
{
    public sealed class SuperFrameRenderer : ViewRenderer<ContentView, Windows.UI.Xaml.Controls.ContentControl>
    {
        SuperFrame superFrame;
        Windows.UI.Xaml.Media.CompositeTransform arrowTransform;
        Windows.UI.Xaml.Controls.ContentControl contentControl;
        Windows.UI.Xaml.Shapes.Path arrowPath;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            if (e.OldElement != null)
            {
                superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                superFrame = null;
                arrowTransform = null;
                contentControl = null;
                arrowPath = null;
            }

            if (e.NewElement != null)
            {
                superFrame = e.NewElement as SuperFrame;

                superFrame.DrawArrowRequested += OnDrawArrowRequest;

                if (Control == null)
                {
                    contentControl = new Windows.UI.Xaml.Controls.ContentControl();

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

        private void OnDrawArrowRequest(object sender, EventArgs e)
        {
            DrawArrow();
        }

        private void DrawArrow()
        {
            if (arrowTransform == null || arrowPath == null)
            {
                arrowPath = contentControl.GetVisualChild<Windows.UI.Xaml.Shapes.Path>();
                arrowPath.Fill = new Windows.UI.Xaml.Media.SolidColorBrush(
                    Windows.UI.Color.FromArgb(
                        (byte)(superFrame.ArrowColor.A * 255),
                        (byte)(superFrame.ArrowColor.R * 255),
                        (byte)(superFrame.ArrowColor.G * 255),
                        (byte)(superFrame.ArrowColor.B * 255)));
                arrowTransform = arrowPath.RenderTransform as Windows.UI.Xaml.Media.CompositeTransform;
            }

            var angle = 0;
            var x = 0d;
            var y = 0d;

            switch (superFrame.HorizontalArrowAlignment)
            {
                case SuperPopupSample.HorizontalAlignment.Left:
                    x = arrowPath.Width * 1.5 - superFrame.Width / 2;
                    break;

                case SuperPopupSample.HorizontalAlignment.Center:
                    x = 0;
                    break;

                case SuperPopupSample.HorizontalAlignment.Right:
                    x = superFrame.Width / 2 - arrowPath.Width * 1.5;
                    break;
            }

            switch (superFrame.ArrowDirection)
            {
                case ArrowDirection.Up:
                    y = -superFrame.Height / 2 - arrowPath.Width / 2;
                    break;
                case ArrowDirection.Down:
                    y = superFrame.Height / 2 + arrowPath.Width / 2;
                    angle = 180;
                    break;
            }

            arrowTransform.Rotation = angle - 90;
            arrowTransform.TranslateX = x;
            arrowTransform.TranslateY = y;
        }
    }
}
