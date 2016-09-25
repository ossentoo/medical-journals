using System;

namespace MedicalJournals.Models.Data
{
    public class SubscriptionDetail
    {
        public int SubscriptionDetailId { get; set; }

        public Guid SubscriptionId { get; set; }

        public Guid JournalId { get; set; }

        public virtual Journal Journal { get; set; }

        public virtual Subscription Subscription { get; set; }
    }
}