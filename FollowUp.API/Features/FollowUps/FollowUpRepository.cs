using FollowUp.API.Features.DataBases.Contexts;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.FollowUps
{
    public interface IFollowUpRepository
    {
        Task<IEnumerable<FollowUp>?> GetByAssistance(string identifierKey);
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
                    "Failed to persist new followup for identifier {@IdentifierKey} from {@UserId}", 
                    entity.IdentifierKey, 
                    entity.Author?.Id);
                
                return false;
            }
        }

        public async Task<IEnumerable<FollowUp>?> GetByAssistance(
            string identifierKey)
        {
            try
            {
                List<FollowUp>? retrievedFollowUps = await _ctx
                    .Set<FollowUp>()
                    .Where(followup => followup.IdentifierKey == identifierKey)
                    .Include(followup => followup.Tags)
                    .ToListAsync();

                return retrievedFollowUps;
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to query followups for identifier {@IdentifierKey}", 
                    identifierKey);
                
                return null;
            }
        }
    }
}
