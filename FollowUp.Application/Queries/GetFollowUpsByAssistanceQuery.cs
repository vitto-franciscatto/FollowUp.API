using FollowUp.Application.DTOs;
using LanguageExt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Queries
{
    public class GetFollowUpsByAssistanceQuery
        : IRequest<Result<IEnumerable<FollowUpDTO>>>
    {
        public int AssistanceId { get; set; } = 0;
    }
}
