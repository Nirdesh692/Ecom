using Ecom.Models;
using Ecom.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecom.Areas.Identity.Pages.Account.Manage
{
    public class OrdersModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;
        private readonly IShippingDetailsService _shippingDetailsService;

        public OrdersModel(UserManager<User> userManager, IOrderService orderService, IShippingDetailsService shippingDetailsService)
        {
            _userManager = userManager;
            _orderService = orderService;
            _shippingDetailsService = shippingDetailsService;
        }

        public IEnumerable<Order> Orders { get; set; }
        public Dictionary<Guid, ShippingDetail> ShippingDetailsByOrderId { get; set; } = new Dictionary<Guid, ShippingDetail>();

        public async Task<IActionResult> OnGet(string status)
        {
            var user = await _userManager.GetUserAsync(User);

            Orders = _orderService.GetOrder(user.Id).Where(o => o.OrderStatus == status);

            foreach (var order in Orders)
            {
                var shippingDetail = _shippingDetailsService.GetShippingDetails(user.Id)
                    .FirstOrDefault(s => s.OrderId == order.Id);

                if (shippingDetail != null)
                {
                    ShippingDetailsByOrderId[order.Id] = shippingDetail;
                }
            }

            return Page();
        }
    }
}
