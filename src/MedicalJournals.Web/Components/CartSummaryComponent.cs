using System.Threading.Tasks;
using MedicalJournals.Entities;
using MedicalJournals.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MedicalJournals.Entities.Data;

namespace MedicalJournals.Web.Components
{
    [ViewComponent(Name = "CartSummary")]
    public class CartSummaryComponent : ViewComponent
    {
        public CartSummaryComponent(JournalContext context)
        {
            DbContext = context;
        }

        private JournalContext DbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = Cart.GetCart(DbContext, HttpContext);
            
            var cartItems = await cart.GetCartAlbumTitles();

            ViewBag.CartCount = cartItems.Count;
            ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

            return View();
        }
    }
}