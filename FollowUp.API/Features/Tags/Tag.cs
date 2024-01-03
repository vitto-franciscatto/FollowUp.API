using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FollowUp.API.Features.Tags
{
    public class Tag
    {
        private Tag(){}

        private int _id;
        private string _name = default!;
        private List<FollowUps.FollowUp> _followUps = default!;

        public static Tag Construct()
        {
            var tag = new Tag();
            tag._followUps = new List<FollowUps.FollowUp>();

            return tag;
        }

        public int Id
        {
            get => _id; 
            set => _id = value;
        }

        public string Name
        {
            get => _name; 
            set => _name = value;
        }

        public List<FollowUps.FollowUp> FollowUps
        {
            get => _followUps; 
            set => _followUps = value;
        }

        public static Tag Create(int id, string name)
        {
            Tag tag = Construct();
            tag._id = id;
            tag._name = name;

            return tag;
        }
    }
    
    public class TagDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        public static explicit operator Tag(TagDTO? tagDto)
        {
            if (tagDto is null)
            {
                return default!;
            }
            
            var tag = Tag.Construct();
            tag.Id = tagDto.Id;
            tag.Name = tagDto.Name;

            return tag;
        }
        
        public static explicit operator TagDTO(Tag? tag)
        {
            if (tag is null)
            {
                return default!;
            }
            
            var tagDto = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return tagDto;
        }
    }
    
    public class TagMap: IEntityTypeConfiguration<Tag>
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
