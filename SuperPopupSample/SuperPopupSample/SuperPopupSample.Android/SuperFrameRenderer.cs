using SuperPopupSample;
using SuperPopupSample.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportRenderer(typeof(SuperFrame), typeof(SuperFrameRenderer))]
namespace SuperPopupSample.Droid
{
    public class SuperFrameRenderer : FrameRenderer
    {
        SuperFrame _superFrame;
        ArrowOptions _arrowOptions;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                _superFrame = null;
                _arrowOptions = null;
            }

            if (e.NewElement != null)
            {
                _superFrame = e.NewElement as SuperFrame;

                _superFrame.DrawArrowRequested += OnDrawArrowRequest;
            }
        }

        void OnDrawArrowRequest(object sender, ArrowOptions request)
        {
            DrawArrow(request);
        }

        void DrawArrow(ArrowOptions request)
        {
            _arrowOptions = request;
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (_arrowOptions != null)
            {
                var paint = new Paint()
                {
                    Color = _arrowOptions.Color.ToAndroid()
                };

                var x = Helpers.DpToPx(_arrowOptions.Location.X, ViewGroup.Context.Resources.DisplayMetrics);
                var y = Helpers.DpToPx(_arrowOptions.Location.Y, ViewGroup.Context.Resources.DisplayMetrics);
                var size = Helpers.DpToPx(_arrowOptions.Size, ViewGroup.Context.Resources.DisplayMetrics);

                DrawTriangle(canvas, paint, (float)x, (float)y, (float)size, (float)_arrowOptions.RotationAngle);
            }

            base.OnDraw(canvas);
        }

        public void DrawTriangle(Canvas canvas, Paint paint, float x, float y, float size, float angle)
        {
            var trianglePath = new Path();
            trianglePath.MoveTo(x, y); // Top
            trianglePath.LineTo(x - size, y + size); // Bottom left
            trianglePath.LineTo(x + size, y + size); // Bottom right
            trianglePath.LineTo(x, y); // Back to Top
            trianglePath.Close();

            var matrix = new Matrix();
            matrix.PostRotate(angle, x, y);
            trianglePath.Transform(matrix);

            canvas.DrawPath(trianglePath, paint);
        }
    }
}