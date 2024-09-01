using Ecom.Models;

namespace Ecom.Services
{
    public interface ICartService
    {
        Cart GetCart(string userId);
        void AddToCart(string userId, Guid productId, int quantity);
        void RemoveFromCart(string userId, Guid cartItemId);
        void ClearCart(string userId);
    }
}
