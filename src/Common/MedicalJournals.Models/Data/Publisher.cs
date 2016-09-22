using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Models
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
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Journal> Journals { get; set; }
    }
}