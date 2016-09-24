using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Models.Data
{
    [Table("Publishers")]
    public class Publisher
    {
        public Publisher()
        {
            PublisherId = Guid.NewGuid();
            Created = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        public Guid PublisherId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Country Country { get; set; }
        public virtual ICollection<Journal> Journals { get; set; }
    }
}