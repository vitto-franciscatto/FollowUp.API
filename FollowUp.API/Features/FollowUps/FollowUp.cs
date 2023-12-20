using FollowUp.API.Features.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FollowUp.API.Features.FollowUps
{
    public class FollowUp
    {
        //empty constructor to make ef happy
        public FollowUp(){}

        public int Id { get; private set; }
        public string IdentifierKey { get; private set; }
        public Author? Author { get; private set; }
        public Contact? Contact { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime OccuredAt { get; private set; }
        public List<Tag>? Tags { get; private set; }
        
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
            var newFollowup = new FollowUp
            {
                Id = id,
                IdentifierKey = identifierKey,
                Author = author,
                Contact = contact,
                Message = message,
                CreatedAt = createdAt,
                OccuredAt = occuredAt,
                Tags = tags
            };
            
            return newFollowup;
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