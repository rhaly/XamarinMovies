using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Ninject;
using ReactiveUI;
using XamarinMovies.Common.ViewModel;
using XamarinMovies.Droid.Controls;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace XamarinMovies.Droid.Views
{
    [Activity(Label = "XamarinMovies.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MovieListView : RxBaseActivity<IMovieListViewModel>
    {
        public RecyclerView MoviesList { get; private set; }

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

            //InitSwipeRefreshLayout();

            InitRecyclerView();
        }

        private void InitRecyclerView()
        {
            MoviesList.AddItemDecoration(new MarginDecoration(this));
            MoviesList.HasFixedSize = true;
            //RecyclerView.SetLayoutManager(new GridLayoutManager(this, 4, GridLayoutManager.Vertical, false));
            MoviesList.SetAdapter(new MovieListAdapter(ViewModel.Movies));

        }
    }
}


