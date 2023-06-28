using FollowUp.Application.Interfaces;
using FollowUp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FollowUp.Infra.Repos
{
    public class FollowUpRepository : IFollowUpRepository
    {
        private readonly FollowUpDbContext _ctx;
        public FollowUpRepository(
            FollowUpDbContext ctx
            )
        {
            _ctx = ctx;
        }

        public async Task<Domain.FollowUp> CreateAsync(Domain.FollowUp entity)
        {
            FollowUpDAL dal = entity.MapToFollowUpDAL();
            await _ctx.Set<FollowUpDAL>().AddAsync(dal);
            await _ctx.SaveChangesAsync();

            IEnumerable<int>? tagIds = dal.Tags?.Select(_ => _.TagId);
            List<TagDAL>? tags = tagIds is null ? 
                null : 
                await _ctx.Set<TagDAL>().Where(_ => tagIds.Contains(_.Id)).ToListAsync();

            dal.Tags = dal.Tags?.Select(_ => 
            {
                TagDAL? thisTag = tags?.Single(tag => tag.Id == _.TagId);

                return new DALs.FollowUpTag()
                {
                    FollowUpId = dal.Id, 
                    FollowUp = dal, 
                    TagId = _.TagId, 
                    Tag = thisTag
                };
            }).ToList();

            return dal.MapToFollowUp();
        }

        public async Task<IEnumerable<Domain.FollowUp>?> GetByAssistance(int assistanceId)
        {
            IEnumerable<FollowUpDAL>? dals = await _ctx
                .Set<FollowUpDAL>()
                .Where(_ => _.AssistanceId == assistanceId)
                .Include(_ => _.Tags)
                .ThenInclude(_ => _.Tag)
                .ToListAsync();

            return dals.Select(_ => _.MapToFollowUp());
        }
    }
}
