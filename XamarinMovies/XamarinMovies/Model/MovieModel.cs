using ReactiveUI;
using XamarinMovies.Contracts.Dto;

namespace XamarinMovies.Common.Model
{
    public class MovieModel : BaseModel, IMovieModel
    {
        public MovieModel(Movie dto)
        {
            Dto = dto;
        }

        public Movie Dto { get; }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { this.RaiseAndSetIfChanged(ref _imageUrl, value); }
        }
    }
}