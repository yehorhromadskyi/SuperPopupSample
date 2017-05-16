using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public static class Gestures
    {
        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.CreateAttached("TappedCommand",
                                            typeof(ICommand),
                                            typeof(Gestures),
                                            default(ICommand),
                                            propertyChanged: OnTappedCommandChanged);

        public static ICommand GetTappedCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(TappedCommandProperty);
        }

        public static void SetTappedCommand(BindableObject view, ICommand value)
        {
            view.SetValue(TappedCommandProperty, value);
        }

        private static void OnTappedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view)
            {
                if (!view.Effects.Any(e => e is TapGestureEffect))
                {
                    view.Effects.Add(new TapGestureEffect());
                }
            }
        }
    }

    class TapGestureEffect : RoutingEffect
    {
        public TapGestureEffect() : base("SuperForms.TapGestureEffect")
        {
        }
    }
}
