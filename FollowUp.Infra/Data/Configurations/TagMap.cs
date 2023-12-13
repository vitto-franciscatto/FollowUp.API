using FollowUp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FollowUp.Infra.Data.Configurations
{
    internal class TagMap 
        : IEntityTypeConfiguration<Tag>
    {
        public void Configure(
            EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags", "followup");

            builder.HasKey(tag => tag.Id);

            builder.Property(tag => tag.Id)
                .IsRequired()
                .UseIdentityColumn(1, 1)
                .HasColumnName("Id")
                .HasColumnType("int");

            builder.Property(tag => tag.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(255)");

            builder.HasMany(tag => tag.FollowUps)
                .WithMany(followup => followup.Tags);
        }
    }
}
