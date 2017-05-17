using SuperPopupSample.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("SuperForms")]
[assembly: ExportEffect(typeof(TapGestureEffect), nameof(TapGestureEffect))]
namespace SuperPopupSample.UWP
{
    public class TapGestureEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            Control.Tapped += OnTapped;
        }

        protected override void OnDetached()
        {
            Control.Tapped -= OnTapped;
        }

        private void OnTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var command = Gestures.GetTappedCommand(Element);
            if (command != null)
            {
                var foundationPoint = e.GetPosition(Control);
                var positionOnTheScreen = (Control as Windows.UI.Xaml.UIElement)
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
