using SuperPopupSample;
using SuperPopupSample.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SuperFrame), typeof(SuperFrameRenderer))]
namespace SuperPopupSample.UWP
{
    public sealed class SuperFrameRenderer : ViewRenderer<ContentView, Windows.UI.Xaml.Controls.ContentControl>
    {
        SuperFrame _superFrame;
        Windows.UI.Xaml.Media.CompositeTransform _arrowTransform;
        Windows.UI.Xaml.Controls.ContentControl _contentControl;
        Windows.UI.Xaml.Shapes.Path _arrowPath;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            if (e.OldElement != null)
            {
                _superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                _superFrame = null;
                _arrowTransform = null;
                _contentControl = null;
                _arrowPath = null;
            }

            if (e.NewElement != null)
            {
                _superFrame = e.NewElement as SuperFrame;

                _superFrame.DrawArrowRequested += OnDrawArrowRequest;

                if (Control == null)
                {
                    _contentControl = new Windows.UI.Xaml.Controls.ContentControl();

                    var style = App.Current.Resources["ArrowedContentViewStyle"] as Windows.UI.Xaml.Style;
                    if (style != null)
                    {
                        _contentControl.Style = style;
                    }

                    SetNativeControl(_contentControl);
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
            if (_arrowTransform == null || _arrowPath == null)
            {
                _arrowPath = _contentControl.GetVisualChild<Windows.UI.Xaml.Shapes.Path>();
                _arrowPath.Fill = new Windows.UI.Xaml.Media.SolidColorBrush(
                    Windows.UI.Color.FromArgb(
                        (byte)(_superFrame.ArrowColor.A * 255),
                        (byte)(_superFrame.ArrowColor.R * 255),
                        (byte)(_superFrame.ArrowColor.G * 255),
                        (byte)(_superFrame.ArrowColor.B * 255)));
                _arrowTransform = _arrowPath.RenderTransform as Windows.UI.Xaml.Media.CompositeTransform;
            }

            var angle = 0;
            var x = options.Location.X - _superFrame.Width / 2;
            var y = options.Location.Y - _superFrame.Height / 2;

            switch (options.Direction)
            {
                case ArrowDirection.Up:
                    y += _arrowPath.Width / 2;
                    break;
                case ArrowDirection.Down:
                    y -= _arrowPath.Width / 2;
                    angle = 180;
                    break;
            }

            _arrowTransform.Rotation = angle - 90;
            _arrowTransform.TranslateX = x;
            _arrowTransform.TranslateY = y;
        }
    }
}
