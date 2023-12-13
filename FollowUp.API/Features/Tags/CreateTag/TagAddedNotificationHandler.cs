using FollowUp.API.Features.Caches;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.Tags.CreateTag
{
    internal class TagAddedNotificationHandler 
        : INotificationHandler<TagAddedNotification>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;
        
        public TagAddedNotificationHandler(ICacheService cacheService, ILogger logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Handle(
            TagAddedNotification notification, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _cacheService.RemoveAsync(
                    "followUpsAPI_tags", 
                    cancellationToken);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@NotificationName}", nameof(TagAddedNotification));
            }
        }
    }
}
