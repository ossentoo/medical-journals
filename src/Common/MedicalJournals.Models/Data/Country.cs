using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalJournals.Models.Data
{
    [Table("Countries")]
    public class Country
    {
        public Country()
        {
            Publishers = new List<Publisher>();
        }

        public int CountryId { get; set; }

        [StringLength(255, MinimumLength = 1)]
        public string CountryName { get; set; }
        [StringLength(3, MinimumLength = 1)]
        public string CountryCode { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Publisher> Publishers { get; set; }

    }
}