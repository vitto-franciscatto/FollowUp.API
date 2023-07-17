using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using LanguageExt.Pipes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace FollowUp.Application.Handlers.NotificationHandler
{
    internal class FollowUpAddedNotificationHandler 
        : INotificationHandler<FollowUpAddedNotification>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;
        
        public FollowUpAddedNotificationHandler(
            ICacheService cacheService, ILogger logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Handle(
            FollowUpAddedNotification notification, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _cacheService.RemoveAsync(
                    $"followUpsAPI_followUps_assistance_{notification.FollowUp.AssistanceId}", 
                    cancellationToken);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@NotificationName}", nameof(FollowUpAddedNotification));
            }
        }
    }
}
