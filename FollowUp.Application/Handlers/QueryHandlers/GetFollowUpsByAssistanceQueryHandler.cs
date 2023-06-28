using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Queries.GetFollowUpByAssistance;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Handlers.QueryHandlers
{
    public class GetFollowUpsByAssistanceQueryHandler : IRequestHandler<GetFollowUpsByAssistanceQuery, Result<IEnumerable<FollowUpDTO>>>
    {
        private readonly IFollowUpRepository _repository;

        public GetFollowUpsByAssistanceQueryHandler(IFollowUpRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<FollowUpDTO>>> Handle(GetFollowUpsByAssistanceQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.FollowUp>? followUps = await _repository.GetByAssistance(request.AssistanceId);

            if (followUps is null)
            {
                return new Result<IEnumerable<FollowUpDTO>>(Enumerable.Empty<FollowUpDTO>());
            }

            return new Result<IEnumerable<FollowUpDTO>>(followUps!.Select(_ => _.MapToFollowUpDTO()));
        }
    }
}
