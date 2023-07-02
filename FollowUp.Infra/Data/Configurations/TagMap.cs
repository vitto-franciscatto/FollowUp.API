using FollowUp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Infra.Data.Configurations
{
    internal class TagMap 
        : IEntityTypeConfiguration<TagDAL>
    {
        public void Configure(
            EntityTypeBuilder<TagDAL> builder)
        {
            builder.ToTable("Tags", "tag");

            builder.HasKey(x => x.Id);

            builder.Property(_ => _.Id)
                .IsRequired()
                .UseIdentityColumn(1, 1)
                .HasColumnName("Id")
                .HasColumnType("int");

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(255)");

            builder.HasMany(_ => _.FollowUps)
                .WithOne(x => x.Tag)
                .HasForeignKey(_ => _.TagId)
                .HasConstraintName("FK_FollowUps_Tag")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
