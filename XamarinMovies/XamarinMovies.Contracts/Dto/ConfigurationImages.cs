using Newtonsoft.Json;

namespace XamarinMovies.Contracts.Dto
{
    public class ConfigurationImages
    {
        [JsonProperty("base_url")]
        public string BaseUrl { get; set; }

        [JsonProperty("secure_base_url")]
        public string SecureBaseUrl { get; set; }

        [JsonProperty("backdrop_sizes")]
        public string[] BackdropSizes { get; set; }

        [JsonProperty("logo_sizes")]
        public string[] LogoSizes { get; set; }

        [JsonProperty("poster_sizes")]
        public string[] PosterSizes { get; set; }

        [JsonProperty("profile_sizes")]
        public string[] ProfileSizes { get; set; }

        [JsonProperty("still_sizes")]
        public string[] StillSizes { get; set; }
    }

}

