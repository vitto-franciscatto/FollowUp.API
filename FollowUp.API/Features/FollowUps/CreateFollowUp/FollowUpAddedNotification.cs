using MediatR;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
{
    public class FollowUpAddedNotification : INotification
    {
        public FollowUp FollowUp { get; set; } = default!;
    }
}
