using FollowUp.Infra.DALs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Infra.Data.Configurations
{
    internal class FollowUpTagMap : IEntityTypeConfiguration<FollowUpTag>
    {
        public void Configure(EntityTypeBuilder<FollowUpTag> builder)
        {
            builder.ToTable("FollowUpTag", "followUp");

            builder.HasKey(x => new { x.FollowUpId, x.TagId });

            builder.Property(_ => _.FollowUpId)
                .IsRequired()
                .HasColumnName("FollowUpId")
                .HasColumnType("int");

            builder.Property(_ => _.TagId)
                .IsRequired()
                .HasColumnName("TagId")
                .HasColumnType("int");
        }
    }
}
