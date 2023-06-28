using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.DTOs
{
    internal static class ContactMapper
    {
        internal static Contact? MapToContact (this ContactDTO? dto)
        {
            if (dto is null)
            {
                return null;
            }

            return Contact.Create(
                dto.Name, 
                dto.PhoneNumber, 
                dto.Job);
        }

        internal static ContactDTO? MapToContact(this Contact? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new ContactDTO() { Name = entity.Name, PhoneNumber = entity.PhoneNumber, Job = entity.Job };
        }
    }
}
