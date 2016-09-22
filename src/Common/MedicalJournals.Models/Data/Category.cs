using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalJournals.Models.Data
{
    [Table("Categories")]

    public class Category
    {
        public Category()
        {
            Journals = new List<Journal>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Journal> Journals { get; set; }
    }
}
