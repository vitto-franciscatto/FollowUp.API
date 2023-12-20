using FollowUp.API.Features.Caches;
using LanguageExt.Common;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.Tags.GetAllTags
{
    public class GetAllTagsQueryHandler 
        
        : IRequestHandler<GetAllTagsQuery, Result<IEnumerable<Tag>?>>
    {
        private readonly ITagRepository _repository;
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;

        public GetAllTagsQueryHandler(
            ITagRepository repository, 
            ICacheService cacheService, ILogger logger)
        {
            _repository = repository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<Tag>?>> Handle(
            GetAllTagsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Tag>? response = 
                    await _cacheService.GetAsync<IEnumerable<Tag>>(
                        "followUpsAPI_tags", 
                        async () => 
                        {
                            IEnumerable<Tag>? tags = 
                                await _repository.Get();
                            return tags;
                        }, 
                        cancellationToken);

                return new Result<IEnumerable<Tag>?>(response);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@QueryName}", nameof(GetAllTagsQuery));

                return new Result<IEnumerable<Tag>?>(error);
            }
        }
    }
}
