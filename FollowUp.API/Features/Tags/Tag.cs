using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FollowUp.API.Features.Tags
{
    public class Tag
    {
        private Tag(
            int id, 
            string name) 
        {
            Id = id;
            Name = name;
        }
        
        public int Id { get; private set; }
        public string Name { get; private set; }

        public List<FollowUps.FollowUp> FollowUps { get; private set; }

        public static Tag Create(int id, string name)
        {
            return new Tag(id, name);
        }
    }
    
    public class TagMap 
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
