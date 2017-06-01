using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SuperPopupSample.UWP
{
    public static class VisualTreeExtensions
    {
        public static T GetVisualChild<T>(this UIElement parent)
            where T : FrameworkElement
        {
            if (parent == null) return null;

            if (parent is T)
            {
                return (T)parent;
            }

            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (child is T)
                {
                    return (T)child;
                }

                result = child.GetVisualChild<T>();
            }

            return result;
        }
    }
}
