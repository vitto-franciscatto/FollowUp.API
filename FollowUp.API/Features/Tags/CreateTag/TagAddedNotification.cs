using MediatR;

namespace FollowUp.API.Features.Tags.CreateTag
{
    public class TagAddedNotification : INotification
    {
        public Tag Tag { get; set; }
    }
}
