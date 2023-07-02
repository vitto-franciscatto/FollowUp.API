using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Queries;
using FollowUp.Domain;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Handlers.QueryHandlers
{
    public class GetAllTagsQueryHandler 
        
        : IRequestHandler<GetAllTagsQuery, Result<IEnumerable<TagDTO>?>>
    {
        private readonly ITagRepository _repository;
        private readonly ICacheService _cacheService;

        public GetAllTagsQueryHandler(
            ITagRepository repository, 
            ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<TagDTO>?>> Handle(
            GetAllTagsQuery request, 
            CancellationToken cancellationToken)
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
    }
}
