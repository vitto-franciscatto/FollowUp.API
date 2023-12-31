﻿// <auto-generated />
using System;
using FollowUp.API.Features.DataBases.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    [DbContext(typeof(FollowUpDbContext))]
    [Migration("20231213124744_remove-dals")]
    partial class removedals
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FollowUp.Domain.FollowUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssistanceId")
                        .HasColumnType("int")
                        .HasColumnName("AssistanceId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Message");

                    b.Property<DateTime>("OccuredAt")
                        .HasColumnType("datetime")
                        .HasColumnName("OccuredAt");

                    b.HasKey("Id");

                    b.ToTable("FollowUps", "followup");
                });

            modelBuilder.Entity("FollowUp.Domain.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags", "followup");
                });

            modelBuilder.Entity("FollowUpTag", b =>
                {
                    b.Property<int>("FollowUpsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("FollowUpsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("FollowUpTag", "followup");
                });

            modelBuilder.Entity("FollowUp.Domain.FollowUp", b =>
                {
                    b.OwnsOne("FollowUp.Domain.Author", "Author", b1 =>
                        {
                            b1.Property<int>("FollowUpId")
                                .HasColumnType("int");

                            b1.Property<string>("Extension")
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("AuthorExtension");

                            b1.Property<int>("Id")
                                .HasColumnType("int")
                                .HasColumnName("AuthorId");

                            b1.HasKey("FollowUpId");

                            b1.ToTable("FollowUps", "followup");

                            b1.WithOwner()
                                .HasForeignKey("FollowUpId");
                        });

                    b.OwnsOne("FollowUp.Domain.Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("FollowUpId")
                                .HasColumnType("int");

                            b1.Property<string>("Job")
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("ContactJob");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("ContactName");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(19)")
                                .HasColumnName("ContactPhoneNumber");

                            b1.HasKey("FollowUpId");

                            b1.ToTable("FollowUps", "followup");

                            b1.WithOwner()
                                .HasForeignKey("FollowUpId");
                        });

                    b.Navigation("Author");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("FollowUpTag", b =>
                {
                    b.HasOne("FollowUp.Domain.FollowUp", null)
                        .WithMany()
                        .HasForeignKey("FollowUpsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FollowUp.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
