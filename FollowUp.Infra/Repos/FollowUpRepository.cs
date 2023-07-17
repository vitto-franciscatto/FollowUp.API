using FollowUp.Application.Interfaces;
using FollowUp.Infra.DALs;
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

        public async Task<Domain.FollowUp?> CreateAsync(
            Domain.FollowUp entity)
        {
            try
            {
                FollowUpDAL dal = entity.MapToFollowUpDAL();
                await _ctx.Set<FollowUpDAL>().AddAsync(dal);
                await _ctx.SaveChangesAsync();

                IEnumerable<int>? tagIds = dal.Tags?
                    .Select(_ => _.TagId);
                List<TagDAL>? tags = tagIds is null ? 
                    null : 
                    await _ctx
                        .Set<TagDAL>()
                        .Where(_ => tagIds.Contains(_.Id))
                        .ToListAsync();

                dal.Tags = dal.Tags?.Select(_ => 
                {
                    TagDAL? thisTag = tags?
                        .Single(tag => tag.Id == _.TagId);

                    return new FollowUpTag()
                    {
                        FollowUpId = dal.Id, 
                        FollowUp = dal, 
                        TagId = _.TagId, 
                        Tag = thisTag
                    };
                }).ToList();

                return dal.MapToFollowUp();
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to persist new followup from assistance {@AssistanceId} from {@UserId}", 
                    entity.AssistanceId, 
                    entity.Author?.Id);
                
                return null;
            }
        }

        public async Task<IEnumerable<Domain.FollowUp>?> GetByAssistance(
            int assistanceId)
        {
            try
            {
                IEnumerable<FollowUpDAL>? dals = await _ctx
                    .Set<FollowUpDAL>()
                    .Where(_ => _.AssistanceId == assistanceId)
                    .Include(_ => _.Tags)
                    .ThenInclude(_ => _.Tag)
                    .ToListAsync();

                return dals.Select(_ => _.MapToFollowUp());
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
