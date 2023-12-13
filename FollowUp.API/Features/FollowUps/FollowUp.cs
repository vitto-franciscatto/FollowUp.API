using FollowUp.API.Features.FollowUps;
using FollowUp.API.Features.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps
{
    public class FollowUp
    {
        public FollowUp()
        {
        }

        private FollowUp(
            int id, 
            int assistanceId, 
            Author? author, 
            Contact? contact, 
            string message, 
            DateTime createdAt, 
            DateTime occuredAt, 
            List<Tag>? tags)
        {
            Id = id;
            AssistanceId = assistanceId;
            Author = author;
            Contact = contact;
            Message = message;
            CreatedAt = createdAt;
            OccuredAt = occuredAt;
            Tags = tags;
        }

        public int Id { get; private set; }
        public int AssistanceId { get; private set; }
        public Author? Author { get; private set; }
        public Contact? Contact { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime OccuredAt { get; private set; }
        public List<Tag>? Tags { get; private set; }

        public static FollowUp Create(
            int id, 
            int assistanceId,
            Author? author,
            Contact? contact,
            string message,
            DateTime createdAt,
            DateTime occuredAt,
            List<Tag>? tags)
        {
            return new FollowUp(
                id,
                assistanceId,
                author,
                contact,
                message,
                createdAt,
                occuredAt,
                tags);
        }
    }
    
    public class FollowUpDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("assistanceId")]
        public int AssistanceId { get; set; } = 0;

        [JsonProperty("author")]
        public AuthorDTO? Author { get; set; }

        [JsonProperty("contact")]
        public ContactDTO? Contact { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;

        [JsonProperty("occuredAt")]
        public DateTime OccuredAt { get; set; } = DateTime.MinValue;

        [JsonProperty("tags")]
        public IEnumerable<TagDTO>? Tags { get; set; }
    }
    
    public static class FollowUpDTOMapper
    {
        public static FollowUpDTO MapToFollowUpDTO(
            this FollowUp entity)
        {
            return new FollowUpDTO() 
            {
                Id = entity.Id, 
                AssistanceId = entity.AssistanceId, 
                Author = entity.Author is null ?
                    null 
                    : entity.Author.MapToAuthor(), 
                Contact = entity.Contact.MapToContact(), 
                Message = entity.Message, 
                CreatedAt = entity.CreatedAt, 
                OccuredAt = entity.OccuredAt,
                Tags = entity.Tags?
                    .Select(_ => _.MapToTagDTO())
            };
        }

        public static FollowUp MapToFollowUp(
            this FollowUpDTO dto)
        {
            return FollowUp.Create(
                dto.Id, 
                dto.AssistanceId, 
                dto.Author is null ? 
                    null 
                    : dto.Author.MapToAuthor(), 
                dto.Contact is null ? 
                    null 
                    : dto.Contact.MapToContact(), 
                dto.Message, 
                dto.CreatedAt, 
                dto.OccuredAt, 
                dto.Tags?
                    .Select(_ => _!.MapToTag())
                    .ToList()
            );
        }
    }
    
    public class FollowUpMap 
        : IEntityTypeConfiguration<FollowUp>
    {
        public void Configure(
            EntityTypeBuilder<FollowUp> builder)
        {
            builder.ToTable("FollowUps", "followup");

            builder.HasKey(followup => followup.Id);

            builder.Property(followup => followup.Id)
                .IsRequired()
                .UseIdentityColumn(1, 1)
                .HasColumnName("Id")
                .HasColumnType("int");

            builder.Property(followup => followup.AssistanceId)
                .IsRequired()
                .HasColumnName("AssistanceId")
                .HasColumnType("int");

            builder.OwnsOne(followup => followup.Author)
                .Property(author => author.Id)
                .IsRequired()
                .HasColumnName("AuthorId")
                .HasColumnType("int");

            builder.OwnsOne(followup => followup.Author)
                .Property(author => author.Extension)
                .IsRequired(false)
                .HasColumnName("AuthorExtension")
                .HasColumnType("nvarchar(50)");

            builder.OwnsOne(followup => followup.Contact)
                .Property(contact => contact.Name)
                .IsRequired(false)
                .HasColumnName("ContactName")
                .HasColumnType("nvarchar(500)");

            builder.OwnsOne(followup => followup.Contact)
                .Property(contact => contact.PhoneNumber)
                .IsRequired(false)
                .HasColumnName("ContactPhoneNumber")
                .HasColumnType("nvarchar(19)");

            builder.OwnsOne(followup => followup.Contact)
                .Property(contact => contact.Job)
                .IsRequired(false)
                .HasColumnName("ContactJob")
                .HasColumnType("nvarchar(255)");

            builder.Property(followup => followup.Message)
                .IsRequired()
                .HasColumnName("Message")
                .HasColumnType("nvarchar(4000)");

            builder.Property(followup => followup.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime");

            builder.Property(followup => followup.OccuredAt)
                .IsRequired()
                .HasColumnName("OccuredAt")
                .HasColumnType("datetime");
        }
    }
}