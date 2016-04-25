using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Ninject;
using ReactiveUI;
using XamarinMovies.Common;
using XamarinMovies.Common.ViewModel;
using XamarinMovies.Droid.Controls;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace XamarinMovies.Droid.Views
{
    [Activity(Label = "XamarinMovies.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MovieListView : RxBaseActivity<IMovieListViewModel>
    {
        private MovieListAdapter _movieListAdapter;

        public RecyclerView MoviesList { get; private set; }

        public SwipeRefreshLayout SwipeRefreshLayout { get; private set; }

        public Toolbar Toolbar { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MoviesList);
            ViewModel = MoviesApplication.Container.Get<IMovieListViewModel>();
            ViewModel.View = this;
            this.WireUpControls();

            Toolbar.Title = "Xamarin Movies";
            SetSupportActionBar(Toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            this.OneWayBind(ViewModel, vm => vm.IsLoading, v => v.SwipeRefreshLayout.Refreshing);
            this.BindCommand(ViewModel, vm => vm.RefreshCommand, v => v.SwipeRefreshLayout, "Refresh");
            InitSwipeRefreshLayout();

            InitRecyclerView();
        }

        private void InitSwipeRefreshLayout()
        {
            SwipeRefreshLayout.SetProgressViewOffset(false, Toolbar.Height, Resources.DisplayMetrics.DipsToPix(64) + Toolbar.Height);
            SwipeRefreshLayout.SetColorSchemeColors(Resource.Color.colorAccent);
            //SwipeRefreshLayout.Refresh += (sender, args) => ViewModel.RefreshCommand.Execute(null);
        }

        private void InitRecyclerView()
        {
            MoviesList.AddItemDecoration(new MarginDecoration(this));
            MoviesList.HasFixedSize = true;
            //RecyclerView.SetLayoutManager(new GridLayoutManager(this, 4, GridLayoutManager.Vertical, false));
            _movieListAdapter = new MovieListAdapter(ViewModel.Movies, MoviesApplication.Container.Get<IScheduleProvider>());
            MoviesList.SetAdapter(_movieListAdapter);

        }

        protected override void OnDestroy()
        {
            _movieListAdapter.Dispose();
            base.OnDestroy();            
        }
    }
}


