using LanguageExt.Common;
using MediatR;

namespace FollowUp.API.Features.FollowUps.GetFollowUpsByAssistance
{
    public class GetFollowUpsByAssistanceQuery
        : IRequest<Result<IEnumerable<FollowUpDTO>>>
    {
        public int AssistanceId { get; set; } = 0;
    }
}
