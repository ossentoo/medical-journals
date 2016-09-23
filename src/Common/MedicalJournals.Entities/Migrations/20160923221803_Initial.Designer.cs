using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MedicalJournals.Entities;

namespace MedicalJournals.Entities.Migrations
{
    [DbContext(typeof(JournalContext))]
    [Migration("20160923221803_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedicalJournals.Identity.JournalRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Application", b =>
                {
                    b.Property<string>("ApplicationId");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Secret");

                    b.HasKey("ApplicationId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryCode")
                        .HasAnnotation("MaxLength", 3);

                    b.Property<string>("CountryName")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<bool>("IsEnabled");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Journal", b =>
                {
                    b.Property<Guid>("JournalId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BlockCount");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("FileName");

                    b.Property<long?>("FileSize");

                    b.Property<short?>("Height");

                    b.Property<bool?>("IsEnabled");

                    b.Property<bool?>("IsParentalAdvisory");

                    b.Property<bool?>("IsPublic");

                    b.Property<bool?>("IsUploadComplete");

                    b.Property<DateTime>("LastModified");

                    b.Property<DateTime?>("LastViewed");

                    b.Property<string>("OverviewThumbnailPath");

                    b.Property<string>("QueryId");

                    b.Property<byte?>("ResolutionId");

                    b.Property<int?>("TimeSpan");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("UploadFinishTime");

                    b.Property<DateTime?>("UploadStartTime");

                    b.Property<string>("UploadStatusMessage")
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<Guid>("UserId");

                    b.Property<long?>("ViewCount");

                    b.Property<short?>("Width");

                    b.HasKey("JournalId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.JournalTag", b =>
                {
                    b.Property<Guid>("JournalId");

                    b.Property<int>("TagId");

                    b.HasKey("JournalId", "TagId");

                    b.HasIndex("JournalId");

                    b.HasIndex("TagId");

                    b.ToTable("JournalTags");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Publisher", b =>
                {
                    b.Property<Guid>("PublisherId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountryId");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsEnabled");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.Property<Guid?>("UserId");

                    b.HasKey("PublisherId");

                    b.HasIndex("CountryId");

                    b.HasIndex("UserId");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Subscription", b =>
                {
                    b.Property<Guid>("SubscriptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsEnabled");

                    b.Property<Guid?>("JournalId");

                    b.Property<DateTime>("LastModified");

                    b.Property<Guid?>("UserId");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("JournalId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MedicalJournals.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Journal", b =>
                {
                    b.HasOne("MedicalJournals.Models.Data.Category", "Category")
                        .WithMany("Journals")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MedicalJournals.Models.Data.Publisher", "Publisher")
                        .WithMany("Journals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.JournalTag", b =>
                {
                    b.HasOne("MedicalJournals.Models.Data.Journal", "Journal")
                        .WithMany("JournalTags")
                        .HasForeignKey("JournalId");

                    b.HasOne("MedicalJournals.Models.Data.Tag", "Tag")
                        .WithMany("JournalTags")
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Publisher", b =>
                {
                    b.HasOne("MedicalJournals.Models.Data.Country")
                        .WithMany("Publishers")
                        .HasForeignKey("CountryId");

                    b.HasOne("MedicalJournals.Models.Identity.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MedicalJournals.Models.Data.Subscription", b =>
                {
                    b.HasOne("MedicalJournals.Models.Data.Journal", "Journal")
                        .WithMany()
                        .HasForeignKey("JournalId");

                    b.HasOne("MedicalJournals.Models.Identity.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("MedicalJournals.Identity.JournalRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MedicalJournals.Models.Identity.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MedicalJournals.Models.Identity.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("MedicalJournals.Identity.JournalRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MedicalJournals.Models.Identity.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
