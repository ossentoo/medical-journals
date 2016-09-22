using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalJournals.Models.Data
{
    [Table("Tags")]

    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<JournalTag> JournalTags { get; set; }

    }
}
