using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;

namespace XamarinMovies.Droid.Controls
{
    public class MarginDecoration : RecyclerView.ItemDecoration
    {
        private readonly int _margin;

        public MarginDecoration(Context context)
        {
            _margin = context.Resources.GetDimensionPixelSize(Resource.Dimension.item_margin);
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            outRect.Set(_margin, _margin, _margin, _margin);
        }
    }
}