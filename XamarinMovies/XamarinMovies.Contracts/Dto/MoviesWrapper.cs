using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinMovies.Contracts.Dto
{
    public class MoviesWrapper
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public IList<Movie> Movies { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}