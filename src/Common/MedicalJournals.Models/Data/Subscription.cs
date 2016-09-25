
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalJournals.Models.Identity;

namespace MedicalJournals.Models.Data
{
    [Table("Subscriptions")]
    public class Subscription
    {
        public Subscription()
        {
            SubscriptionId = Guid.NewGuid();
            Created = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        public Guid SubscriptionId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public bool HasExpired { get; set; }

        public decimal Total { get; set; }
        public List<SubscriptionDetail> SubscriptionDetails { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Journal Journal { get; set; }

    }
}
