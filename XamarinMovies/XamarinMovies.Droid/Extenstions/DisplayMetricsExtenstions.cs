using Android.Content.Res;
using Android.Util;

// ReSharper disable once CheckNamespace
namespace Android.Content.Res
{
    public static class DisplayMetricsExtenstions
    {
        public static int DipsToPix(this DisplayMetrics metrics, float dps)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dps, metrics);
        }
    }
}