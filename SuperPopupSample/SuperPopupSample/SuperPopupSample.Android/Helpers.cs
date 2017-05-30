using Android.Content;

namespace SuperPopupSample.Droid
{
    public static class Helpers
    {
        public static Xamarin.Forms.Point PxToDp(Xamarin.Forms.Point point, Android.Util.DisplayMetrics displayMetrics)
        {
            point.X = point.X / displayMetrics.Density;
            point.Y = point.Y / displayMetrics.Density;

            return point;
        }

        public static double DpToPx(double dp, Android.Util.DisplayMetrics displayMetrics)
        {
            return dp * displayMetrics.Density;
        }

        public static int GetStatusBarHeight(Context context)
        {
            var statusBarHeight = 0;
            var resourceId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = context.Resources.GetDimensionPixelSize(resourceId);
            }

            return statusBarHeight;
        }
    }
}