using FollowUp.Application.Commands.CreateFollowUp;

namespace FollowUp.API.Requests
{
    internal static class CreateFollowUpRequestMapper
    {
        internal static CreateFollowUpCommand MapToCommand(this CreateFollowUpRequest request, DateTime dateTime)
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
