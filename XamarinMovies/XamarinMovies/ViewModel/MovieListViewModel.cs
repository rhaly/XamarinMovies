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
        private readonly IDialogService _dialogService;
        private readonly IScheduleProvider _scheduleProvider;
        private string _baseImageUrl;

        public MovieListViewModel(IMoviesService moviesService, IDialogService dialogService, IScheduleProvider scheduleProvider)
        {
            _moviesService = moviesService;
            _dialogService = dialogService;
            _scheduleProvider = scheduleProvider;
            RefreshCommand = new DelegateCommand(_ => RefreshMovies());
        }

        private void RefreshMovies()
        {
            LoadMovies()
                   .SubscribeOn(_scheduleProvider.TaskPool)
                   .ObserveOn(_scheduleProvider.UiScheduler)
                   .Subscribe(OnMoviesLoaded, OnError);
        }

        private readonly ReactiveList<IMovieModel> _movies = new ReactiveList<IMovieModel>();

        public IReadOnlyReactiveList<IMovieModel> Movies => _movies;

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set { this.RaiseAndSetIfChanged(ref _isLoading, value); }
        }

        public ICommand RefreshCommand { get; }

        public override void OnNavigatedTo()
        {
            if (Movies.IsEmpty)
            {
                RefreshMovies();
            }
        }

        private void OnMoviesLoaded(IEnumerable<IMovieModel> movies)
        {
            if (!_movies.IsEmpty)
            {
                _movies.Clear();
            }
            _movies.AddRange(movies);
            IsLoading = false;
        }

        private void OnError(Exception exception)
        {
            IsLoading = false;
            _dialogService.ShowMessageDialog(exception.Message, "OK")
                .Subscribe();
        }

        private IObservable<IEnumerable<IMovieModel>> LoadMovies()
        {
            IsLoading = true;
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