using Ninject.Modules;
using Refit;
using XamarinMovies.Common;
using XamarinMovies.Common.Services;

namespace XamarinMovies.Droid
{
    public class DroidModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMoviesApi>().ToMethod(_ => RestService.For<IMoviesApi>(Constans.ApiUrl));
        }
    }
}