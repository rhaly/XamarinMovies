using System.Windows.Input;
using ReactiveUI;
using XamarinMovies.Common.Model;

namespace XamarinMovies.Common.ViewModel
{
    public interface IMovieListViewModel : IRxViewModel
    {
        IReadOnlyReactiveList<IMovieModel> Movies { get; }

        bool IsLoading { get; }

        ICommand RefreshCommand { get; }

    }
}