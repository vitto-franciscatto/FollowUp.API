using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
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
    
    public static class CreateFollowUpRequestMapper
    {
        public static CreateFollowUpCommand MapToCommand(
            this CreateFollowUpRequest request, 
            DateTime dateTime)
        {
            return new CreateFollowUpCommand()
            {
                AssistanceId = request.AssistanceId, 
                Author = request.Author, 
                Contact = request.Contact, 
                Message = request.Message, 
                CreatedAt = dateTime, 
                OccuredAt = request.DateTime, 
                TagIds = request.Tags,
            };
        }
    }
}
