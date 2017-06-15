using CoreAnimation;
using CoreGraphics;
using SuperPopupSample;
using SuperPopupSample.iOS;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SuperFrame), typeof(SuperFrameRenderer))]
namespace SuperPopupSample.iOS
{
    public class SuperFrameRenderer : FrameRenderer
    {
        const float ArrowSize = 10;

        CAShapeLayer triangleLayerOld;
        SuperFrame superFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                superFrame = null;
                triangleLayerOld = null;
            }

            if (e.NewElement != null)
            {
                superFrame = e.NewElement as SuperFrame;

                superFrame.DrawArrowRequested += OnDrawArrowRequest;
            }
        }

        private void OnDrawArrowRequest(object sender, EventArgs args)
        {
            DrawArrow();
        }

        void DrawArrow()
        {
            var triangleLayer = new CAShapeLayer();
            var trianglePath = new UIBezierPath();

            trianglePath.MoveTo(CGPoint.Empty);
            trianglePath.AddLineTo(new CGPoint(-ArrowSize, ArrowSize));
            trianglePath.AddLineTo(new CGPoint(ArrowSize, ArrowSize));
            trianglePath.ClosePath();

            triangleLayer.Path = trianglePath.CGPath;
            triangleLayer.FillColor = superFrame.ArrowColor.ToCGColor();
            triangleLayer.AnchorPoint = CGPoint.Empty;

            var angle = 0;
            var x = 0d;
            var y = 0d;

            switch (superFrame.HorizontalArrowAlignment)
            {
                case SuperPopupSample.HorizontalAlignment.Left:
                    x = ArrowSize * 1.5;
                    break;

                case SuperPopupSample.HorizontalAlignment.Center:
                    x = superFrame.Width / 2 + ArrowSize / 2;
                    break;

                case SuperPopupSample.HorizontalAlignment.Right:
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
                    angle = 180;
                    break;
            }

            var radians = angle * Math.PI / 180;
            triangleLayer.Transform = CATransform3D.MakeRotation((float)radians, 0, 0, 1);

            triangleLayer.Frame = new CGRect(x, y, ArrowSize, ArrowSize);

            if (this.triangleLayerOld != null)
            {
                NativeView.Layer.ReplaceSublayer(this.triangleLayerOld, triangleLayer);
            }
            else
            {
                NativeView.Layer.AddSublayer(triangleLayer);
            }

            this.triangleLayerOld = triangleLayer;
        }
    }
}