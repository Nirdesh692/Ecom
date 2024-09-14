using Ecom.Data;
using Ecom.Models;
using Ecom.Services.Interface;

namespace Ecom.Services
{
    public class OrderService : IOrderService
    {
        private readonly ProductDbContext _context;

        public OrderService(ProductDbContext context)
        {
            _context = context;
        }
        public ICollection<Order> GetOrder(string Id)
        {
            var orders = _context.Orders.Where(o=>o.UserId==Id);
            return orders.ToList();
        }
        public void AddOrders(string Id, double totalAmount)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Id,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void AddShippingDetails(string id, Guid orderId, ShippingDetail shipping)
        {
            var shippingDetail = new ShippingDetail
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ShippingAddress = shipping.ShippingAddress,
                Provience = shipping.Provience,
                City = shipping.City,
                PostalCode = shipping.PostalCode,
                ShippingStatus = "pending"
            };
            _context.ShippingDetails.Add(shippingDetail);
            _context.SaveChanges();
        }
        public void AddOrderItems(ICollection<CartItem> cartItems, Guid orderId, Cart cart)
        {
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = cartItem.ProductId,  
                    Quantity = cartItem.Quantity,    
                    UnitPrice = cartItem.UnitPrice   
                };

                _context.OrderItems.Add(orderItem);
            }
            _context.SaveChanges();
        }
    }
}
