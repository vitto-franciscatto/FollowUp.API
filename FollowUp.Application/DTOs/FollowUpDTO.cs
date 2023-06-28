using FollowUp.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.DTOs
{
    public class FollowUpDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("assistanceId")]
        public int AssistanceId { get; set; } = 0;

        [JsonProperty("author")]
        public AuthorDTO? Author { get; set; }

        [JsonProperty("contact")]
        public ContactDTO? Contact { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;

        [JsonProperty("occuredAt")]
        public DateTime OccuredAt { get; set; } = DateTime.MinValue;

        [JsonProperty("tags")]
        public IEnumerable<TagDTO>? Tags { get; set; }
    }
}
