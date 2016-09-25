using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MedicalJournals.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedicalJournals.Models.Data
{
    //[Bind(Include = "FirstName,LastName,Address,City,State,PostalCode,Country")]
    public class Order
    {
        [BindNever]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public virtual ApplicationUser User{ get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [BindNever]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}