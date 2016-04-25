using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using XamarinMovies.Common.Model;
using XamarinMovies.Contracts.Dto;

namespace XamarinMovies.Common.Services
{
    public interface IMoviesService
    {
        IObservable<string> GetBaseImageUrl();

        IObservable<IEnumerable<IMovieModel>> GetPopularMovies(string imageUrl);

        IObservable<MovieDetails> GetMovieDetail(string id);
    }

    class MoviesService : IMoviesService
    {
        private readonly IMoviesApi _moviesApi;

        public MoviesService(IMoviesApi moviesApi)
        {
            _moviesApi = moviesApi;
        }

        public IObservable<string> GetBaseImageUrl()
        {
            return _moviesApi.GetConfiguration(Constans.ApiKey)                
                .Select(ConfigureBaseUrlImage);
        }

        private string ConfigureBaseUrlImage(ConfigurationResponse configuration)
        {
            string url = string.Empty;

            if (configuration.Images == null)
                return url;

            url = configuration.Images.BaseUrl;
            string imageQuality = string.Empty;

            foreach (var quality in configuration.Images.BackdropSizes)
            {
                if (quality == Constans.DesiredQuality)
                {
                    imageQuality = quality;
                    break;
                }
            }

            if (imageQuality == string.Empty)
                imageQuality = "original";

            url += imageQuality;

            return url;
        }

        public IObservable<IEnumerable<IMovieModel>> GetPopularMovies(string imageUrl)
        {
            return _moviesApi.GetPopularMovies(Constans.ApiKey)
                .Select(response => OnMoviesLoaded(response, imageUrl));
        }

        private IEnumerable<IMovieModel> OnMoviesLoaded(MoviesWrapper response, string imageUrl)
        {
            var list = new List<IMovieModel>(response.Movies.Count);

            response.Movies.ForEach(movie => list.Add(new MovieModel(movie)
            {
                ImageUrl = $"{imageUrl}/{movie.PosterPath}"
            }));

            return list;
        }

        public IObservable<MovieDetails> GetMovieDetail(string id)
        {
            return _moviesApi.GetMovieDetail(Constans.ApiKey, id);
        }
    }
}