using SuperPopupSample;
using SuperPopupSample.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportRenderer(typeof(SuperFrame), typeof(SuperFrameRenderer))]
namespace SuperPopupSample.Droid
{
    public sealed class SuperFrameRenderer : FrameRenderer
    {
        const double ArrowSize = 10;

        SuperFrame superFrame;

        bool drawArrow;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                superFrame = null;
            }

            if (e.NewElement != null)
            {
                superFrame = e.NewElement as SuperFrame;

                superFrame.DrawArrowRequested += OnDrawArrowRequest;
            }
        }

        void OnDrawArrowRequest(object sender, EventArgs args)
        {
            drawArrow = true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (drawArrow)
            {
                var paint = new Paint()
                {
                    Color =  superFrame.ArrowColor.ToAndroid()
                };

                var angle = 0f;
                var x = 0d;
                var y = 0d;

                switch (superFrame.HorizontalArrowAlignment)
                {
                    case HorizontalAlignment.Left:
                        x = ArrowSize * 1.5;
                        break;

                    case HorizontalAlignment.Center:
                        x = superFrame.Width / 2 + ArrowSize / 2;
                        break;

                    case HorizontalAlignment.Right:
                        x = superFrame.Width - ArrowSize * 1.5;
                        break;
                }

                switch (superFrame.ArrowDirection)
                {
                    case ArrowDirection.Up:
                        y = -ArrowSize;
                        break;
                    case ArrowDirection.Down:
                        y = superFrame.Height + ArrowSize;
                        angle = 180f;
                        break;
                }

                var convertedX = Helpers.DpToPx(x, ViewGroup.Context.Resources.DisplayMetrics);
                var convertedY = Helpers.DpToPx(y, ViewGroup.Context.Resources.DisplayMetrics);
                var size = Helpers.DpToPx(ArrowSize, ViewGroup.Context.Resources.DisplayMetrics);

                DrawTriangle(canvas, paint, (float)convertedX, (float)convertedY, (float)size, angle);
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