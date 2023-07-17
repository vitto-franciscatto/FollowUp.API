using FollowUp.Application.Interfaces;
using FollowUp.Domain;
using FollowUp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace FollowUp.Infra.Repos
{
    public class TagRepository : ITagRepository
    {
        private readonly FollowUpDbContext _ctx;
        private readonly ILogger _logger;

        public TagRepository(FollowUpDbContext ctx, ILogger logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<Tag?> CreateAsync(Tag tag)
        {
            try
            {
                TagDAL dal = tag.MapToTagDAL();
                await _ctx.Set<TagDAL>().AddAsync(dal);
                await _ctx.SaveChangesAsync();

                return dal.MapToTag();
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to persist new Tag with name {@TagName}", 
                    tag.Name);

                return null;
            }
        }

        public async Task<IEnumerable<Tag>?> Get()
        {
            try
            {
                List<TagDAL> dals = await _ctx
                    .Set<TagDAL>()
                    .ToListAsync();

                return dals.Select(dal => dal.MapToTag());
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to query Tags");
                
                return null;
            }
        }

        public async Task<Tag?> Get(int tagId)
        {
            try
            {
                TagDAL? dal = await _ctx
                    .Set<TagDAL>()
                    .SingleOrDefaultAsync(_ => _.Id == tagId);

                return dal?.MapToTag();
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to query Tag with Id {@TagId}", 
                    tagId);
                
                return null;
            }
        }
    }
}
