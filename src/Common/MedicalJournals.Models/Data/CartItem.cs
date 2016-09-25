using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalJournals.Models.Data
{
    public class CartItem
    {
        public CartItem()
        {
            Created= DateTime.UtcNow;    
        }

        [Key]
        public int CartItemId { get; set; }

        [Required]
        public string CartId { get; set; }
        public Guid JournalId { get; set; }
        public int Count { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public virtual Journal Journal { get; set; }
    }
}