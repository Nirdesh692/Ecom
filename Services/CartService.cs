using Ecom.Data;
using Ecom.Models;
using Ecom.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services
{
    public class CartService : ICartService
    {
        private readonly ProductDbContext _context;

        public CartService(ProductDbContext context)
        {
            _context = context;
        }
        public Cart GetCart(string id)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == id);
        }
        public void AddToCart(string UserId, Guid ProductId, int quantity)
        {
            var cart = GetCart(UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    CartItems = new List<CartItem>(),
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();

            }
            var cartItem = cart.CartItems.FirstOrDefault(c=>c.ProductId == ProductId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = ProductId,
                    Quantity = quantity,
                    UnitPrice = _context.Products.Single(p=>p.Id == ProductId).Price
                };
                _context.CartItems.Add(cartItem);
                cart.CartItems.Add(cartItem);
                _context.SaveChanges();
            }
            else
            {
                cartItem.Quantity += quantity;
                _context.SaveChanges();
            }
        }

        public void UpdateCartQuantity(string userId, Guid productId, int quantity)
        {
            var cart = GetCart(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Cart item not found");
            }
        }

        public void RemoveFromCart(string UserId, Guid cartItemId)
        {
            var cart = GetCart(UserId);
            if(cart == null)
            {
                return;
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci=>ci.Id == cartItemId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        } 

        public void UpdateQuantity(string UserId, Guid cartItemId, int quantity)
        {
            var cart = GetCart(UserId);
            if (cart == null)
            {
                return;
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity+=quantity;
                if (cartItem.Quantity == 0)
                {
                    cart.CartItems.Remove(cartItem);
                }
                _context.SaveChanges();
            }
        }

        public void ClearCart(string UserId)
        {
            var cart = GetCart(UserId);
            if (cart == null)
            {
                return;
            }
            _context.CartItems.RemoveRange(cart.CartItems);
            _context.SaveChanges();
        }
    }
}
