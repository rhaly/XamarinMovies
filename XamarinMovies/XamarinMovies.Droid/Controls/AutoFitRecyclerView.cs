using System;
using Android.Content;
using Android.Content.Res;
using Android.Support.V7.Widget;
using Android.Util;

namespace XamarinMovies.Droid.Controls
{
    public class AutoFitRecyclerView : RecyclerView
    {
        private GridLayoutManager _manager;
        private int _columnWidth = -1;

        public AutoFitRecyclerView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Init(context, attrs);
        }

        public AutoFitRecyclerView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init(context, attrs);
        }

        public AutoFitRecyclerView(Context context)
            : base(context)
        {
            Init(context, null);
        }

        private void Init(Context context, IAttributeSet attributes)
        {
            if (attributes != null)
            {
                int[] attributesArray = { Android.Resource.Attribute.ColumnWidth };
                TypedArray array = context.ObtainStyledAttributes(attributes, attributesArray);
                _columnWidth = array.GetDimensionPixelSize(0, -1);
                array.Recycle();
            }
            _manager = new GridLayoutManager(Context, 2);
            SetLayoutManager(_manager);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            if (_columnWidth > 0)
            {
                int spanCount = Math.Max(1, MeasuredWidth / _columnWidth);
                _manager.SpanCount = spanCount;
            }
        }
    }
}