using Newtonsoft.Json;

namespace FollowUp.API.Features.Tags.CreateTag
{
    public class CreateTagRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
    
    public static class CreateTagRequestMapper
    {
        public static CreateTagCommand MapToCommand(
            this CreateTagRequest entity)
        {
            return new CreateTagCommand() 
            { 
                Name = entity.Name 
            };
        }
    }
}
