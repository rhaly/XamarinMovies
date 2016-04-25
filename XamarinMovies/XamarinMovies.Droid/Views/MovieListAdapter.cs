using System;
using System.Reactive.Disposables;
using System.Threading;
using Android.Animation;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ReactiveUI;
using XamarinMovies.Common.Model;

namespace XamarinMovies.Droid.Views
{
    public class MovieListAdapter : RecyclerView.Adapter
    {
        private readonly IReadOnlyReactiveList<IMovieModel> _list;
        private IDisposable _inner;
        private int _previousPosition;

        public MovieListAdapter(IReadOnlyReactiveList<IMovieModel> list)
        {
            _list = list;
            _inner = _list.Changed.Subscribe(_ => NotifyDataSetChanged());
            HasStableIds = true;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _list[position];
            var view = (ViewHolder)holder;

            view.BindToViewModel(item);
            SlidingAnimation(view, position > _previousPosition);
            _previousPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MovieListItem, parent, false);
            var viewHolder = new ViewHolder(view);

            return viewHolder;
        }

        public override int ItemCount => _list.Count;

        public override long GetItemId(int position)
        {
            return _list[position].Dto.Id;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Interlocked.Exchange(ref _inner, Disposable.Empty).Dispose();
        }

        private static void SlidingAnimation(ViewHolder holder, bool goesDown)
        {
            ObjectAnimator animatorTranslateY = ObjectAnimator.OfFloat(holder.ItemContent, "translationY", goesDown ? 100 : -100, 0);

            animatorTranslateY.SetDuration(500);

            animatorTranslateY.Start();
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public FrameLayout ItemContent { get; }

            public ImageView ImageView { get; }

            public TextView TitleLabel { get; }

            public IMovieModel ViewModel { get; private set; }

            public ViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {            
            }

            public ViewHolder(View itemView) : base(itemView)
            {
                ItemContent = itemView.FindViewById<FrameLayout>(Resource.Id.itemContent);
                TitleLabel = itemView.FindViewById<TextView>(Resource.Id.itemMovieTitle);
                ImageView = itemView.FindViewById<ImageView>(Resource.Id.itemMovieCover);
            }

            public void BindToViewModel(IMovieModel model)
            {
                ViewModel = model;
                TitleLabel.Text = model.Dto.Title;
            }
        }
    }
}