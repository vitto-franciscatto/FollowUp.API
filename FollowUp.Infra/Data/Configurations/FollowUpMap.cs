using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FollowUp.Infra.Data.Configurations
{
    public class FollowUpMap 
        : IEntityTypeConfiguration<Domain.FollowUp>
    {
        public void Configure(
            EntityTypeBuilder<Domain.FollowUp> builder)
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
