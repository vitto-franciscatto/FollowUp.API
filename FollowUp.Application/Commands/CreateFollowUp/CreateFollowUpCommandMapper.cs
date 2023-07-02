using FollowUp.Application.DTOs;
using FollowUp.Domain;

namespace FollowUp.Application.Commands.CreateFollowUp
{
    internal static class CreateFollowUpCommandMapper
    {
        internal static Domain.FollowUp MapToFollowUp(
            this CreateFollowUpCommand command)
        {
            return Domain.FollowUp.Create(
                0,
                command.AssistanceId, 
                command.Author.MapToAuthor(), 
                command.Contact.MapToContact(), 
                command.Message, 
                command.CreatedAt, 
                command.OccuredAt, 
                command.TagIds?
                    .Select(id => Tag.Create(id, string.Empty))
                    .ToList()
                );
        }
    }
}
