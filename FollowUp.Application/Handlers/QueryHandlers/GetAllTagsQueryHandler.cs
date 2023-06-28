using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Queries;
using FollowUp.Domain;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Handlers.QueryHandlers
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<IEnumerable<TagDTO>>>
    {
        private readonly ITagRepository _repository;

        public GetAllTagsQueryHandler(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<TagDTO>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Tag> response = await _repository.Get();

            return new Result<IEnumerable<TagDTO>>(response.Select(tag => tag.MapToTagDTO()));
        }
    }
}
