using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Commands.CreateTag
{
    internal static class CreateTagCommandMapper
    {
        internal static Tag MapToTag(this CreateTagCommand command)
        {
            return Tag.Create(0, command.Name);
        }
    }
}
