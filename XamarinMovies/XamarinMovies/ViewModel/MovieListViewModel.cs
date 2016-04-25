using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Newtonsoft.Json.Serialization;
using ReactiveUI;
using XamarinMovies.Common.Model;
using XamarinMovies.Common.Services;

namespace XamarinMovies.Common.ViewModel
{
    public class MovieListViewModel : RxViewModel, IMovieListViewModel
    {
        private readonly IMoviesService _moviesService;
        private readonly IScheduleProvider _scheduleProvider;
        private string _baseImageUrl;
        
        public MovieListViewModel(IMoviesService moviesService, IScheduleProvider scheduleProvider)
        {
            _moviesService = moviesService;
            _scheduleProvider = scheduleProvider;
        }

        private readonly ReactiveList<IMovieModel> _movies = new ReactiveList<IMovieModel>();

        public IReadOnlyReactiveList<IMovieModel> Movies => _movies;

        public bool IsLoading { get; }

        public ICommand RefreshCommand { get; }

        public override void OnNavigatedTo()
        {
            if (Movies.IsEmpty)
            {
                LoadMovies()
                    .SubscribeOn(_scheduleProvider.TaskPool)
                    .ObserveOn(_scheduleProvider.UiScheduler)
                    .Subscribe(movies => _movies.AddRange(movies), OnError);
            }
        }

        private void OnError(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        private IObservable<IEnumerable<IMovieModel>> LoadMovies()
        {
            if (_baseImageUrl == null)
            {
                return _moviesService.GetBaseImageUrl()
                    .SelectMany(url =>
                    {
                        _baseImageUrl = url;
                        return _moviesService.GetPopularMovies(_baseImageUrl);
                    });
            }

            return _moviesService.GetPopularMovies(_baseImageUrl);
        }
    }
}