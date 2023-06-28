using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.DTOs
{
    internal static class FollowUpDTOMapper
    {
        internal static FollowUpDTO MapToFollowUpDTO(this Domain.FollowUp entity)
        {
            return new FollowUpDTO() 
            {
                Id = entity.Id, 
                AssistanceId = entity.AssistanceId, 
                Author = entity.Author is null ? null : entity.Author.MapToAuthor(), 
                Contact = entity.Contact.MapToContact(), 
                Message = entity.Message, 
                CreatedAt = entity.CreatedAt, 
                OccuredAt = entity.OccuredAt,
                Tags = entity.Tags?.Select(_ => _.MapToTagDTO())
            };
        }

        internal static Domain.FollowUp MapToFollowUp(this FollowUpDTO dto)
        {
            return Domain.FollowUp.Create(
                dto.Id, 
                dto.AssistanceId, 
                dto.Author is null ? null : dto.Author.MapToAuthor(), 
                dto.Contact is null ? null : dto.Contact.MapToContact(), 
                dto.Message, 
                dto.CreatedAt, 
                dto.OccuredAt, 
                dto.Tags?.Select(_ => _!.MapToTag()).ToList()
                );
        }
    }
}
