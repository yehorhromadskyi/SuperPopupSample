using SuperPopupSample.UWP;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("SuperForms")]
[assembly: ExportEffect(typeof(TapGestureEffect), nameof(TapGestureEffect))]
namespace SuperPopupSample.UWP
{
    public class TapGestureEffect : PlatformEffect
    {
        private FrameworkElement _view;

        protected override void OnAttached()
        {
            _view = Control ?? Container;
            _view.Tapped += OnTapped;
        }

        protected override void OnDetached()
        {
            _view.Tapped -= OnTapped;
            _view = null;
        }

        private void OnTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var command = Gestures.GetTappedCommand(Element);
            if (command != null)
            {
                var foundationPoint = e.GetPosition(_view);
                var positionOnTheScreen = (_view)
                    .TransformToVisual(Windows.UI.Xaml.Window.Current.Content)
                    .TransformPoint(foundationPoint);
                var point = new Point(positionOnTheScreen.X, positionOnTheScreen.Y);

                if (command.CanExecute(point))
                {
                    command.Execute(point);
                }
            }
        }
    }
}
