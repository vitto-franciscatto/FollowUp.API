using FollowUp.API.Features.Caches;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
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
                    $"followUpsAPI_followUps_identifierKey_{notification.FollowUp.IdentifierKey}", 
                    cancellationToken);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@NotificationName}", nameof(FollowUpAddedNotification));
            }
        }
    }
}
