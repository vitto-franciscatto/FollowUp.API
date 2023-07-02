using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Handlers.NotificationHandler
{
    internal class TagAddedNotificationHandler 
        : INotificationHandler<TagAddNotification>
    {
        private readonly ICacheService _cacheService;
        public TagAddedNotificationHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(
            TagAddNotification notification, 
            CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync(
                "followUpsAPI_tags", 
                cancellationToken);
        }
    }
}
