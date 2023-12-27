using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
{
    public class CreateFollowUpRequest
    {   
        [JsonProperty("identifierKey")]
        public string IdentifierKey { get; set; } = string.Empty;

        [JsonProperty("author")]
        public Author? Author { get; set; }

        [JsonProperty("contact")]
        public Contact? Contact { get; set; }

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
                IdentifierKey = request.IdentifierKey, 
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
