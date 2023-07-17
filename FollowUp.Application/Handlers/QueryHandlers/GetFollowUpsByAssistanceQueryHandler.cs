using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Queries;
using LanguageExt.Common;
using MediatR;
using Serilog;

namespace FollowUp.Application.Handlers.QueryHandlers
{
    public class GetFollowUpsByAssistanceQueryHandler 
        
        : IRequestHandler<
            GetFollowUpsByAssistanceQuery, 
            Result<IEnumerable<FollowUpDTO>>>
    {
        private readonly IFollowUpRepository _repository;
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;

        public GetFollowUpsByAssistanceQueryHandler(
            IFollowUpRepository repository, 
            ICacheService cacheService, ILogger logger)
        {
            _repository = repository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<FollowUpDTO>>> Handle(
            GetFollowUpsByAssistanceQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<FollowUpDTO>? followUps = 
                    await _cacheService.GetAsync<IEnumerable<FollowUpDTO>>(
                        $"followUpsAPI_followUps_assistance_{request.AssistanceId}", 
                        async () =>
                        {
                            IEnumerable<Domain.FollowUp>? followUps = 
                                await _repository.GetByAssistance(
                                    request.AssistanceId);

                            return followUps?
                                .Select(followUp => followUp.MapToFollowUpDTO());
                        },
                        cancellationToken);

                if (followUps is null)
                {
                    return new Result<IEnumerable<FollowUpDTO>>(
                        Enumerable.Empty<FollowUpDTO>());
                }

                return new Result<IEnumerable<FollowUpDTO>>(followUps);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@QueryName}", nameof(GetFollowUpsByAssistanceQuery));

                return new Result<IEnumerable<FollowUpDTO>>(error);
            }
        }
    }
}
