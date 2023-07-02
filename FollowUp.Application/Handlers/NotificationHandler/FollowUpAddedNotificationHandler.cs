using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using LanguageExt.Pipes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Handlers.NotificationHandler
{
    internal class FollowUpAddedNotificationHandler : INotificationHandler<FollowUpAddedNotification>
    {
        private readonly ICacheService _cacheService;
        public FollowUpAddedNotificationHandler(
            ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(FollowUpAddedNotification notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync($"followUpsAPI_followUps_assistance_{notification.FollowUp.AssistanceId}", cancellationToken);
        }
    }
}
