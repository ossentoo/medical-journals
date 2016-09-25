using System;

namespace MedicalJournals.Models.Data
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public Guid JournalId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Journal Journal { get; set; }

        public virtual Order Order { get; set; }
    }
}