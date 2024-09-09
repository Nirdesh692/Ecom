using Ecom.Models;
using Ecom.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Services.Interface
{
    public interface IOrderService
    {
        //Task<CheckoutView> CheckoutViewAsync(string userId);
        Order GetOrder(string Id);
        void AddOrders(string Id, double totalAmount);
        void AddShippingDetails(string id, Guid orderId, ShippingDetail shippingDetail);
        void AddOrderItems(ICollection<CartItem> cartItems, Guid orderId, Cart cart);
    }
}
