using FollowUp.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FollowUp.Infra.Data.Context
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
