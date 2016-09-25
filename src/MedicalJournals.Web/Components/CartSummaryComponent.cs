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
        private readonly JournalContext _context;

        public CartSummaryComponent([FromServices]JournalContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = Cart.GetCart(_context, HttpContext);
            
            var cartItems = await cart.GetCartJournals();

            ViewBag.CartCount = cartItems.Count;
            ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

            return View();
        }
    }
}