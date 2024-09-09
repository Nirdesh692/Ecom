using Ecom.Models;

namespace Ecom.Services.Interface
{
    public interface ICartService
    {
        Cart GetCart(string userId);
        void AddToCart(string userId, Guid productId, int quantity);
        public void UpdateQuantity(string UserId, Guid cartItemId, int quantity);

        void RemoveFromCart(string userId, Guid cartItemId);
        void ClearCart(string userId);
    }
}
