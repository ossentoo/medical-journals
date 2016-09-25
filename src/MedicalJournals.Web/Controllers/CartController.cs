using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MedicalJournals.Entities;
using MedicalJournals.Entities.Data;
using MedicalJournals.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalJournals.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(JournalContext context, ILogger<ShoppingCartController> logger)
        {
            context = context;
            _logger = logger;
        }

        public JournalContext context { get; }

        //
        // GET: /ShoppingCart/
        public async Task<IActionResult> Index()
        {
            var cart = Cart.GetCart(context, HttpContext);

            // Set up our ViewModel
            var viewModel = new CartViewModel
            {
                CartItems = await cart.GetCartItems(),
                CartTotal = await cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /ShoppingCart/AddToCart/5

        public async Task<IActionResult> AddToCart(Guid id, CancellationToken requestAborted)
        {
            // Retrieve the joiurnal from the database
            var journal = await context.Journals
                .SingleAsync(x => x.JournalId == id, cancellationToken: requestAborted);

            // Add it to the shopping cart
            var cart = Cart.GetCart(context, HttpContext);

            await cart.AddToCart(journal);

            await context.SaveChangesAsync(requestAborted);
            _logger.LogInformation("Journal {journalId} was added to the cart.", journal.JournalId);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(
            int id,
            CancellationToken requestAborted)
        {
            // Retrieve the current user's shopping cart
            var cart = Cart.GetCart(context, HttpContext);

            // Get the name of the journal to display confirmation
            var cartItem = await context.CartItems
                .Where(item => item.CartItemId == id)
                .Include(c => c.Journal)
                .SingleOrDefaultAsync();

            string message;
            int itemCount;
            if (cartItem != null)
            {
                // Remove from cart
                itemCount = cart.RemoveFromCart(id);

                await context.SaveChangesAsync(requestAborted);

                string removed = (itemCount > 0) ? " 1 copy of " : string.Empty;
                message = removed + cartItem.Journal.Title + " has been removed from your cart.";
            }
            else
            {
                itemCount = 0;
                message = "Could not find this item, nothing has been removed from your cart.";
            }

            // Display the confirmation message

            var results = new CartRemoveViewModel
            {
                Message = message,
                CartTotal = await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            _logger.LogInformation("Journal {id} was removed from a cart.", id);

            return Json(results);
        }
    }
}