using LanguageExt.Common;
using MediatR;

namespace FollowUp.API.Features.FollowUps.GetFollowUpsByAssistance
{
    public class GetFollowUpsByAssistanceQuery
        : IRequest<Result<IEnumerable<FollowUp>>>
    {
        public string IdentifierKey { get; set; } = string.Empty;
    }
}
