using System;
using Refit;
using XamarinMovies.Contracts.Dto;

namespace XamarinMovies.Common.Services
{
    [Headers("User-Agent: XamarinMovies")]
    public interface IMoviesApi
    {
        [Get("/configuration?api_key={apiKey}")]
        IObservable<ConfigurationResponse> GetConfiguration(string apiKey);

        [Get("/movie/popular?api_key={apiKey}")]
        IObservable<MoviesWrapper> GetPopularMovies(string apiKey);

        [Get("/movie/{id}?api_key={apiKey}")]
        IObservable<MovieDetails> GetMovieDetail(string apiKey, string id);
    }
}