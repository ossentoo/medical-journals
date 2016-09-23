
using System;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalJournals.Models.Data;
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
        public bool IsEnabled { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Journal Journal { get; set; }

    }
}
