using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.DTOs
{
    internal static class AuthorMapper
    {
        internal static Author? MapToAuthor(this AuthorDTO? dto)
        {
            if(dto is null)
            {
                return null;
            }

            return Author.Create(dto.Id, dto.Extension);
        }

        internal static AuthorDTO? MapToAuthor(this Author? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new AuthorDTO() { Id = entity.Id, Extension = entity.Extension };
        }
    }
}
