using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MedicalJournals.Entities;
using MedicalJournals.Entities.Data;
using MedicalJournals.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private const string PromoCode = "FREE";

        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }

        //
        // GET: /Checkout/
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressAndPayment(
            [FromServices] JournalContext context,
            [FromForm] Subscription subscription,
            CancellationToken requestAborted)
        {
            if (!ModelState.IsValid)
            {
                return View(subscription);
            }

            var formCollection = await HttpContext.Request.ReadFormAsync();

            try
            {
                if (string.Equals(formCollection["PromoCode"].FirstOrDefault(), PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(subscription);
                }
                else
                {
                    subscription.User.UserName = HttpContext.User.Identity.Name;
                    subscription.Created = DateTime.UtcNow;

                    //Add the Order
                    context.Subscriptions.Add(subscription);

                    //Process the order
                    var cart = Cart.GetCart(context, HttpContext);
                    await cart.CreateSubscription(subscription);

                    _logger.LogInformation("User {userName} started checkout of {orderId}.", subscription.User.UserName, subscription.SubscriptionId);

                    // Save all changes
                    await context.SaveChangesAsync(requestAborted);

                    return RedirectToAction("Complete", new { id = subscription.SubscriptionId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(subscription);
            }
        }

        //
        // GET: /Checkout/Complete

        public async Task<IActionResult> Complete([FromServices] JournalContext context, Guid id)
        {
            var userName = HttpContext.User.Identity.Name;

            // Validate customer owns this order
            bool isValid = await context.Subscriptions.AnyAsync(
                o => o.SubscriptionId == id &&
                o.User.UserName == userName);

            if (isValid)
            {
                _logger.LogInformation("User {userName} completed checkout on subscription {id}.", userName, id);
                return View(id);
            }
            else
            {
                _logger.LogError(
                    "User {userName} tried to checkout with an order ({id}) that doesn't belong to them.",
                    userName,
                    id);
                return View("Error");
            }
        }
    }
}