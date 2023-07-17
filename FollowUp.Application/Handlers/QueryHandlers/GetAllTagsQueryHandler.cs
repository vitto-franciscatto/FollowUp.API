using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Queries;
using FollowUp.Domain;
using LanguageExt.Common;
using MediatR;
using Serilog;

namespace FollowUp.Application.Handlers.QueryHandlers
{
    public class GetAllTagsQueryHandler 
        
        : IRequestHandler<GetAllTagsQuery, Result<IEnumerable<TagDTO>?>>
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

        public async Task<Result<IEnumerable<TagDTO>?>> Handle(
            GetAllTagsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<TagDTO>? response = 
                    await _cacheService.GetAsync<IEnumerable<TagDTO>>(
                        "followUpsAPI_tags", 
                        async () => 
                        {
                            IEnumerable<Tag>? tags = 
                                await _repository.Get();
                            return tags?
                                .Select(tag => tag.MapToTagDTO());
                        }, 
                        cancellationToken);

                return new Result<IEnumerable<TagDTO>?>(response);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@QueryName}", nameof(GetAllTagsQuery));

                return new Result<IEnumerable<TagDTO>?>(error);
            }
        }
    }
}
