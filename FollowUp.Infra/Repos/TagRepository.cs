using FollowUp.Application.Interfaces;
using FollowUp.Domain;
using FollowUp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Infra.Repos
{
    public class TagRepository : ITagRepository
    {
        private readonly FollowUpDbContext _ctx;

        public TagRepository(FollowUpDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            TagDAL dal = tag.MapToTagDAL();
            await _ctx.Set<TagDAL>().AddAsync(dal);
            await _ctx.SaveChangesAsync();

            return dal.MapToTag();
        }

        public async Task<IEnumerable<Tag>> Get()
        {
            List<TagDAL> dals = await _ctx.Set<TagDAL>().ToListAsync();

            return dals.Select(dal => dal.MapToTag());
        }

        public async Task<Tag?> Get(int tagId)
        {
            TagDAL? dal = await _ctx.Set<TagDAL>().SingleOrDefaultAsync(_ => _.Id == tagId);

            return dal is null ? null : dal.MapToTag();
        }
    }
}
