using FollowUp.API.Features.Caches;
using LanguageExt.Common;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.FollowUps.GetFollowUpsByAssistance
{
    public class GetFollowUpsByAssistanceQueryHandler 
        
        : IRequestHandler<
            GetFollowUpsByAssistanceQuery, 
            Result<IEnumerable<FollowUp>>>
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

        public async Task<Result<IEnumerable<FollowUp>>> Handle(
            GetFollowUpsByAssistanceQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<FollowUp>? followUps = 
                    await _cacheService.GetAsync<IEnumerable<FollowUp>>(
                        $"followUpsAPI_followUps_identifierKey_{request.IdentifierKey}", 
                        async () =>
                        {
                            IEnumerable<FollowUp>? followUps = 
                                await _repository.GetByAssistance(
                                    request.IdentifierKey);

                            return followUps;
                        },
                        cancellationToken);

                if (followUps is null)
                {
                    return new Result<IEnumerable<FollowUp>>(
                        Enumerable.Empty<FollowUp>());
                }

                return new Result<IEnumerable<FollowUp>>(followUps);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@QueryName}", nameof(GetFollowUpsByAssistanceQuery));

                return new Result<IEnumerable<FollowUp>>(error);
            }
        }
    }
}
