using Ecom.Models;
using System.Collections.Generic;

namespace Ecom.ViewModel
{
    public class OrderModel
    {
        public IEnumerable<Order> Orders {  get; set; }
        public ShippingDetail ShippingDetails { get; set; }

    }
}
