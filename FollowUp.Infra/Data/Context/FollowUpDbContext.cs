using FollowUp.Domain;
using FollowUp.Infra.DALs;
using FollowUp.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            modelBuilder
                .ApplyConfiguration<FollowUpDAL>(new FollowUpMap());
            modelBuilder
                .ApplyConfiguration<TagDAL>(new TagMap());
            modelBuilder
                .ApplyConfiguration<FollowUpTag>(new FollowUpTagMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
