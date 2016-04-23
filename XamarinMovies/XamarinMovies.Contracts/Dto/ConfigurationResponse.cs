using Newtonsoft.Json;

namespace XamarinMovies.Contracts.Dto
{
    public class ConfigurationResponse
    {
        [JsonProperty("images")]
        public ConfigurationImages Images { get; set; }

        [JsonProperty("change_keys")]
        public string[] ChangeKeys { get; set; }
    }
}