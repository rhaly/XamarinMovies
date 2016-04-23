using Newtonsoft.Json;

namespace XamarinMovies.Contracts.Dto
{
    public class Movie
    {
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("popularity")]
        public float Popularity { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }
    }
}