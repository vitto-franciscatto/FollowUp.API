using FollowUp.Application.Interfaces;
using FollowUp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FollowUp.Infra.Repos
{
    public class FollowUpRepository : IFollowUpRepository
    {
        private readonly FollowUpDbContext _ctx;
        private readonly ILogger _logger;
        
        public FollowUpRepository(
            FollowUpDbContext ctx, ILogger logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<bool> AddAsync(
            Domain.FollowUp entity)
        {
            try
            {
                await _ctx.Set<Domain.FollowUp>().AddAsync(entity);
                await _ctx.SaveChangesAsync();

                return true;
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to persist new followup from assistance {@AssistanceId} from {@UserId}", 
                    entity.AssistanceId, 
                    entity.Author?.Id);
                
                return false;
            }
        }

        public async Task<IEnumerable<Domain.FollowUp>?> GetByAssistance(
            int assistanceId)
        {
            try
            {
                List<Domain.FollowUp>? retrievedFollowUps = await _ctx
                    .Set<Domain.FollowUp>()
                    .Where(followup => followup.AssistanceId == assistanceId)
                    .Include(followup => followup.Tags)
                    .ToListAsync();

                return retrievedFollowUps;
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to query followups for assistance {@AssistanceId}", 
                    assistanceId);
                
                return null;
            }
        }
    }
}
