using Ninject.Modules;
using Refit;
using XamarinMovies.Common.Services;
using XamarinMovies.Common.ViewModel;

namespace XamarinMovies.Common
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMovieListViewModel>().To<MovieListViewModel>().InSingletonScope();
            Bind<IMoviesService>().To<MoviesService>();
            Bind<IScheduleProvider>().To<ScheduleProvider>().InSingletonScope();

        }
    }
}