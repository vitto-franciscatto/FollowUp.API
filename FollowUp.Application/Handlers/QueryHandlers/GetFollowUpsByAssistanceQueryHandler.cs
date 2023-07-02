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
        private readonly ICacheService _cacheService;

        public GetFollowUpsByAssistanceQueryHandler(
            IFollowUpRepository repository, 
            ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<FollowUpDTO>>> Handle(GetFollowUpsByAssistanceQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<FollowUpDTO>? followUps = await _cacheService.GetAsync<IEnumerable<FollowUpDTO>>(
                $"followUpsAPI_followUps_assistance_{request.AssistanceId}", 
                async () =>
                {
                    IEnumerable<Domain.FollowUp>? followUps = await _repository.GetByAssistance(request.AssistanceId);

                    return followUps?.Select(followUp => followUp.MapToFollowUpDTO());
                },
                cancellationToken);

            if (followUps is null)
            {
                return new Result<IEnumerable<FollowUpDTO>>(Enumerable.Empty<FollowUpDTO>());
            }

            return new Result<IEnumerable<FollowUpDTO>>(followUps);
        }
    }
}
