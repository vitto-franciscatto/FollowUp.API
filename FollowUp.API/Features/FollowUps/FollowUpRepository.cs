using FollowUp.API.Features.DataBases.Contexts;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.FollowUps
{
    public interface IFollowUpRepository
    {
        Task<IEnumerable<FollowUp>?> GetByAssistance(int assistanceId);
        Task<bool> AddAsync(FollowUp entity);
    }
    
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
            FollowUp entity)
        {
            try
            {
                await _ctx.Set<FollowUp>().AddAsync(entity);
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

        public async Task<IEnumerable<FollowUp>?> GetByAssistance(
            int assistanceId)
        {
            try
            {
                List<FollowUp>? retrievedFollowUps = await _ctx
                    .Set<FollowUp>()
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
