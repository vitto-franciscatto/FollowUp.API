using FollowUp.Application.DTOs;
using LanguageExt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<Result<TagDTO>>
    {
        public string Name { get; set; } = string.Empty;
    }
}
