using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Infra
{
    internal static class TagMapper
    {
        internal static Tag MapToTag(this TagDAL dal)
        {
            return Tag.Create(
                dal.Id, 
                dal.Name);
        }

        internal static TagDAL MapToTagDAL(this Tag entity)
        {
            return new TagDAL()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
