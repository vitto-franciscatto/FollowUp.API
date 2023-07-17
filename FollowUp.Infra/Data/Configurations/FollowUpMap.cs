using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FollowUp.Infra.Data.Configurations
{
    public class FollowUpMap 
        : IEntityTypeConfiguration<FollowUpDAL>
    {
        public void Configure(
            EntityTypeBuilder<FollowUpDAL> builder)
        {
            builder.ToTable("FollowUps", "followup");

            builder.HasKey(x => x.Id);

            builder.Property(_ => _.Id)
                .IsRequired()
                .UseIdentityColumn(1, 1)
                .HasColumnName("Id")
                .HasColumnType("int");

            builder.Property(_ => _.AssistanceId)
                .IsRequired()
                .HasColumnName("AssistanceId")
                .HasColumnType("int");

            builder.Property(_ => _.AuthorId)
                .IsRequired()
                .HasColumnName("AuthorId")
                .HasColumnType("int");

            builder.Property(_ => _.AuthorExtension)
                .IsRequired(false)
                .HasColumnName("AuthorExtension")
                .HasColumnType("nvarchar(50)");

            builder.Property(_ => _.ContactName)
                .IsRequired(false)
                .HasColumnName("ContactName")
                .HasColumnType("nvarchar(500)");

            builder.Property(_ => _.ContactPhoneNumber)
                .IsRequired(false)
                .HasColumnName("ContactPhoneNumber")
                .HasColumnType("nvarchar(19)");

            builder.Property(_ => _.ContactJob)
                .IsRequired(false)
                .HasColumnName("ContactJob")
                .HasColumnType("nvarchar(255)");

            builder.Property(_ => _.Message)
                .IsRequired()
                .HasColumnName("Message")
                .HasColumnType("nvarchar(4000)");

            builder.Property(_ => _.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime");

            builder.Property(_ => _.OccuredAt)
                .IsRequired()
                .HasColumnName("OccuredAt")
                .HasColumnType("datetime");

            builder.HasMany(_ => _.Tags)
                .WithOne(x => x.FollowUp)
                .HasForeignKey(_ => _.FollowUpId)
                .HasConstraintName("FK_Tags_FollowUp")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
