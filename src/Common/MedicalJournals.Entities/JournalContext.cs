using System;
using MedicalJournals.Identity;
using MedicalJournals.Models;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MedicalJournals.Entities
{
    public class JournalContext : IdentityDbContext<ApplicationUser, JournalRole, Guid>
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalTag> JournalTags { get; set; }

        public JournalContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.UserName).IsRequired();

                ConfigurePublishers(builder);
                ConfigureJournal(builder);
                ConfigureJournalTag(builder);
            });
        }

        private static void ConfigureJournal(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal>(b =>
            {
                b.HasOne(t => t.Publisher)
                    .WithMany(t => t.Journals)
                    .HasForeignKey(d => d.UserId);

            });
        }

        private static void ConfigurePublishers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>(b =>
            {
                b.HasOne(t => t.User);
            });
        }

        private static void ConfigureJournalTag(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<JournalTag>(b =>
            {
                // Composite key on JournalId and TagId
                b.HasKey(a => new {a.JournalId, a.TagId});

                b.HasOne(t => t.Tag)
                    .WithMany(t => t.JournalTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(t => t.Journal)
                    .WithMany(t => t.JournalTags)
                    .HasForeignKey(d => d.JournalId)
                    .OnDelete(DeleteBehavior.Restrict);

            });
        }
    }
}
