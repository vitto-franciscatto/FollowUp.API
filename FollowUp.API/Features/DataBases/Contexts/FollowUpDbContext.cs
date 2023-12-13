using FollowUp.API.Features.FollowUps;
using FollowUp.API.Features.Tags;
using Microsoft.EntityFrameworkCore;

namespace FollowUp.API.Features.DataBases.Contexts
{
    public class FollowUpDbContext : DbContext
    {
        public FollowUpDbContext(
            DbContextOptions<FollowUpDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FollowUpMap());
            modelBuilder.ApplyConfiguration(new TagMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
