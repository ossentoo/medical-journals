using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalJournals.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Entities.Data
{
    public class Cart
    {
        private readonly JournalContext _context;
        private readonly string _shoppingCartId;

        private Cart(JournalContext dbContext, string id)
        {
            _context = dbContext;
            _shoppingCartId = id;
        }

        public static Cart GetCart(JournalContext db, HttpContext context) 
            => GetCart(db, GetCartId(context));

        public static Cart GetCart(JournalContext db, string cartId)
            => new Cart(db, cartId);

        public async Task AddToCart(Journal journal)
        {
            // Get the matching cart and album instances
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(
                c => c.CartId == _shoppingCartId
                && c.JournalId == journal.JournalId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    JournalId = journal.JournalId,
                    CartId = _shoppingCartId,
                    Count = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _context.CartItems.SingleOrDefault(
                cart => cart.CartId == _shoppingCartId
                && cart.CartItemId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }

            return itemCount;
        }

        public async Task EmptyCart()
        {
            var cartItems = await _context
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .ToArrayAsync();

            _context.CartItems.RemoveRange(cartItems);
        }

        public Task<List<CartItem>> GetCartItems()
        {
            return _context
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .Include(c => c.Journal)
                .ToListAsync();
        }
        
        public Task<List<string>> GetCartAlbumTitles()
        {
            return _context
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .Select(c => c.Journal.Title)
                .OrderBy(n => n)
                .ToListAsync();
        }

        public Task<int> GetCount()
        {
            // Get the count of each item in the cart and sum them up
            return _context
                .CartItems
                .Where(c => c.CartId == _shoppingCartId)
                .Select(c => c.Count)
                .SumAsync();
        }

        public Task<decimal> GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            return _context
                .CartItems
                .Include(c => c.Journal)
                .Where(c => c.CartId == _shoppingCartId)
                .Select(c => c.Journal.Price * c.Count)
                .SumAsync();
        }

        public async Task<int> CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = await GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var journal = await _context.Journals.SingleAsync(a => a.JournalId == item.JournalId);

                var orderDetail = new OrderDetail
                {
                    JournalId = item.JournalId,
                    OrderId = order.OrderId,
                    UnitPrice = journal.Price,
                    Quantity = item.Count,
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * journal.Price);

                _context.OrderDetails.Add(orderDetail);
            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Empty the shopping cart
            await EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to sessions.
        private static string GetCartId(HttpContext context)
        {
            var cartId = context.Session.GetString("Session");

            if (cartId == null)
            {
                //A GUID to hold the cartId. 
                cartId = Guid.NewGuid().ToString();

                // Send cart Id as a cookie to the client.
                context.Session.SetString("Session", cartId);
            }

            return cartId;
        }
    }
}