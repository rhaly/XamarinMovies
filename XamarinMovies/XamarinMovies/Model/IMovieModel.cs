using XamarinMovies.Contracts.Dto;

namespace XamarinMovies.Common.Model
{
    public interface IMovieModel : IBaseModel
    {
        Movie Dto { get; }

        string ImageUrl { get; set; }
    }
}