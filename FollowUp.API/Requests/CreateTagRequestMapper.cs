using FollowUp.Application.Commands.CreateTag;

namespace FollowUp.API.Requests
{
    internal static class CreateTagRequestMapper
    {
        public static CreateTagCommand MapToCommand(this CreateTagRequest entity)
        {
            return new CreateTagCommand() { Name = entity.Name };
        }
    }
}
