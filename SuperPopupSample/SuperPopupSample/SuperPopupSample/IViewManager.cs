using Xamarin.Forms;

namespace SuperPopupSample
{
    public interface IViewManager
    {
        Point GetLocationOnScreen(VisualElement view);
    }
}
