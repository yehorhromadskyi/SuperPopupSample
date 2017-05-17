using System.Threading.Tasks;

namespace SuperPopupSample
{
    public interface IPopup
    {
        Task ShowAsync();

        Task HideAsync();
    }
}
