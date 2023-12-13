using LanguageExt.Common;
using MediatR;

namespace FollowUp.API.Features.Tags.GetAllTags
{
    public class GetAllTagsQuery 
        : IRequest<Result<IEnumerable<TagDTO>?>>
    {
    }
}
