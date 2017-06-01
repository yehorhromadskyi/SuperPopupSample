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

            triangle_Path.MoveTo(CGPoint.Empty);
            triangle_Path.AddLineTo(new CGPoint(-ArrowSize, ArrowSize));
            triangle_Path.AddLineTo(new CGPoint(ArrowSize, ArrowSize));
            triangle_Path.ClosePath();

            triangle_Layer.Path = triangle_Path.CGPath;
            triangle_Layer.FillColor = _superFrame.ArrowColor.ToCGColor();
            triangle_Layer.AnchorPoint = CGPoint.Empty;

            var angle = request.Direction == ArrowDirection.Down ? 180 : 0;
            var radians = angle * Math.PI / 180;
            triangle_Layer.Transform = CATransform3D.MakeRotation((float)radians, 0, 0, 1);
            triangle_Layer.Frame = new CGRect(request.Location.X, request.Location.Y, ArrowSize, ArrowSize);

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