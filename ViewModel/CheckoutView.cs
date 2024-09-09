using Ecom.Models;

namespace Ecom.ViewModel
{
    public class CheckoutView
    {
        public User? User { get; set; }
        public Cart? Cart { get; set; }
        public ShippingDetail? ShippingDetail { get; set; }
        public IEnumerable<CartItem>? CartItems { get; set; }
        public double? TotalAmount { get; set; }
    }
}
