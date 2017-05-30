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
        CAShapeLayer _triangle_Layer;
        SuperFrame _superFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _superFrame.DrawArrowRequested -= OnDrawArrowRequest;

                _superFrame = null;
                _triangle_Layer = null;
            }

            if (e.NewElement != null)
            {
                _superFrame = e.NewElement as SuperFrame;

                _superFrame.DrawArrowRequested += OnDrawArrowRequest;
            }
        }

        private void OnDrawArrowRequest(object sender, ArrowOptions request)
        {
            DrawArrow(request);
        }

        void DrawArrow(ArrowOptions request)
        {
            var triangle_Layer = new CAShapeLayer();
            var triangle_Path = new UIBezierPath();

            var arrowSize = (float)request.Size;
            triangle_Path.MoveTo(CGPoint.Empty);
            triangle_Path.AddLineTo(new CGPoint(-arrowSize, arrowSize));
            triangle_Path.AddLineTo(new CGPoint(arrowSize, arrowSize));
            triangle_Path.ClosePath();

            triangle_Layer.Path = triangle_Path.CGPath;
            triangle_Layer.FillColor = request.Color.ToCGColor();
            triangle_Layer.AnchorPoint = CGPoint.Empty;

            var radians = request.RotationAngle * Math.PI / 180;
            triangle_Layer.Transform = CATransform3D.MakeRotation((float)radians, 0, 0, 1);
            triangle_Layer.Frame = new CGRect(request.Location.X, request.Location.Y, arrowSize, arrowSize);

            if (_triangle_Layer != null)
            {
                NativeView.Layer.ReplaceSublayer(_triangle_Layer, triangle_Layer);
            }
            else
            {
                NativeView.Layer.AddSublayer(triangle_Layer);
            }

            _triangle_Layer = triangle_Layer;
        }
    }
}