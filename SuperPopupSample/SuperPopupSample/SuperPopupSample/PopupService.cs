using System.Collections.Generic;

namespace SuperPopupSample
{
    public static class PopupService
    {
        static readonly Dictionary<PopupType, IPopup> PopupCach = new Dictionary<PopupType, IPopup>();

        public static void Register(PopupType type, IPopup popup)
        {
            if (!PopupCach.ContainsKey(type))
            {
                PopupCach.Add(type, popup);
            }
        }

        public static IPopup Resolve(PopupType type)
        {
            if (PopupCach.ContainsKey(type))
            {
                return PopupCach[type];
            }

            return null;
        }
    }
}
