using Newtonsoft.Json;

namespace FollowUp.API.Requests
{
    public class CreateTagRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
