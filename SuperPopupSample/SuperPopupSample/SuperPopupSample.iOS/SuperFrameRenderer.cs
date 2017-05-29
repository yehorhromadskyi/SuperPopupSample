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
        const int ArrowSize = 10;
        private CAShapeLayer _triangle_Layer;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _triangle_Layer = null;
                (e.OldElement as SuperFrame).DrawArrowRequested -= OnDrawArrowRequest;
            }

            if (e.NewElement != null)
            {
                (e.NewElement as SuperFrame).DrawArrowRequested += OnDrawArrowRequest;
            }
        }

        private void OnDrawArrowRequest(object sender, DrawArrowRequest request)
        {
            DrawArrow(request);
        }

        void DrawArrow(DrawArrowRequest request)
        {
            if (_triangle_Layer == null)
            {
                _triangle_Layer = new CAShapeLayer();
                var triangle_Path = new UIBezierPath();

                triangle_Path.MoveTo(CGPoint.Empty);
                triangle_Path.AddLineTo(new CGPoint(-ArrowSize, ArrowSize));
                triangle_Path.AddLineTo(new CGPoint(ArrowSize, ArrowSize));
                triangle_Path.ClosePath();

                _triangle_Layer.Path = triangle_Path.CGPath;
                _triangle_Layer.FillColor = UIColor.FromRGBA(255, 255, 255, 255).CGColor;
                _triangle_Layer.AnchorPoint = CGPoint.Empty;

                NativeView.Layer.AddSublayer(_triangle_Layer);
            }

            var radians = request.Rotation * Math.PI / 180;
            _triangle_Layer.Transform = CATransform3D.MakeRotation((float)radians, 0, 0, 1);
            _triangle_Layer.Frame = new CGRect(request.Location.X, request.Location.Y, ArrowSize, ArrowSize);
        }
    }
}