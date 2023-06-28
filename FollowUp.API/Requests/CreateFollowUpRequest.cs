using FollowUp.Application.DTOs;
using Newtonsoft.Json;

namespace FollowUp.API.Requests
{
    public class CreateFollowUpRequest
    {

        [JsonProperty("assistanceId")]
        public int AssistanceId { get; set; } = 0;

        [JsonProperty("author")]
        public AuthorDTO? Author { get; set; }

        [JsonProperty("contact")]
        public ContactDTO? Contact { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; } = DateTime.MinValue;

        [JsonProperty("tags")]
        public IEnumerable<int>? Tags { get; set; }
    }
}
