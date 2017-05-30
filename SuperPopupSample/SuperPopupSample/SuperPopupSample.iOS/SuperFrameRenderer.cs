using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using SuperPopupSample.iOS;
using SuperPopupSample;
using CoreAnimation;
using CoreGraphics;

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

        private void OnDrawArrowRequest(object sender, DrawArrowOptions request)
        {
            DrawArrow(request);
        }

        void DrawArrow(DrawArrowOptions request)
        {
            var arrowSize = (float)_superFrame.ArrowSize;
            if (_triangle_Layer == null)
            {
                _triangle_Layer = new CAShapeLayer();
                var triangle_Path = new UIBezierPath();

                triangle_Path.MoveTo(CGPoint.Empty);
                triangle_Path.AddLineTo(new CGPoint(-arrowSize, arrowSize));
                triangle_Path.AddLineTo(new CGPoint(arrowSize, arrowSize));
                triangle_Path.ClosePath();

                _triangle_Layer.Path = triangle_Path.CGPath;
                _triangle_Layer.FillColor = UIColor.FromRGBA(255, 255, 255, 255).CGColor;
                _triangle_Layer.AnchorPoint = CGPoint.Empty;

                NativeView.Layer.AddSublayer(_triangle_Layer);
            }

            var radians = request.Rotation * Math.PI / 180;
            _triangle_Layer.Transform = CATransform3D.MakeRotation((float)radians, 0, 0, 1);
            _triangle_Layer.Frame = new CGRect(request.Location.X, request.Location.Y, arrowSize, arrowSize);
        }
    }
}