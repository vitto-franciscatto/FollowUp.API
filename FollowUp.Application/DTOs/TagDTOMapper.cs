using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.DTOs
{
    internal static class TagDTOMapper
    {
        internal static TagDTO MapToTagDTO(this Tag tag)
        {
            return new TagDTO() 
            {
                Id = tag.Id, 
                Name = tag.Name
            };
        }

        internal static Tag MapToTag(this TagDTO dto)
        {
            return Tag.Create(
                dto.Id, 
                dto.Name);
        }
    }
}
