using System.Collections.Generic;
using MedicalJournals.Models.Data;

namespace MedicalJournals.Models.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
