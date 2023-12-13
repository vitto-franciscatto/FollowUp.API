using FollowUp.Application.Interfaces;
using FollowUp.Domain;
using FollowUp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
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
                await _ctx.Set<Tag>().AddAsync(tag);
                await _ctx.SaveChangesAsync();

                return tag;
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
                List<Tag> retrievedTags = await _ctx
                    .Set<Tag>()
                    .ToListAsync();

                return retrievedTags;
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
                Tag? retrievedTag = await _ctx
                    .Set<Tag>()
                    .SingleOrDefaultAsync(tag => tag.Id == tagId);

                return retrievedTag;
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
