using FollowUp.API.Features.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FollowUp.API.Features.FollowUps
{
    public class FollowUp
    {
        private FollowUp(){}

        public static FollowUp Construct()
        {
            var followUp = new FollowUp();

            return followUp;
        }

        public int Id { get; set; }

        public string IdentifierKey { get; set; } = string.Empty;

        public Author? Author { get; set; }

        public Contact? Contact { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime OccuredAt { get; set; }

        public List<Tag>? Tags { get; set; }

        public static FollowUp Create(
            int id, 
            string identifierKey,
            Author? author,
            Contact? contact,
            string message,
            DateTime createdAt,
            DateTime occuredAt,
            List<Tag>? tags)
        {
            var followUp = Construct();
            followUp.Id = id;
            followUp.IdentifierKey = identifierKey;
            followUp.Author = author;
            followUp.Contact = contact;
            followUp.Message = message;
            followUp.CreatedAt = createdAt;
            followUp.OccuredAt = occuredAt;
            followUp.Tags = tags;
            
            return followUp;
        }
    }
    
    public class FollowUpMap: IEntityTypeConfiguration<FollowUp>
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
            
            builder.Property(followup => followup.IdentifierKey)
                .IsRequired()
                .HasColumnName("IdentifierKey")
                .HasColumnType("nvarchar(450)");

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