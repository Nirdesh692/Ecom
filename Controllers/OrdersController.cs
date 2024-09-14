using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecom.Models;
using Ecom.Data;
using Microsoft.AspNetCore.Identity;
using Ecom.Services.Interface;
using Ecom.ViewModel;

namespace Ecom.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ProductDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IShippingDetailsService _shippingDetailsService;

        public OrdersController(ProductDbContext context, UserManager<User> userManager, IOrderService orderService, ICartService cartService, IShippingDetailsService shippingDetailsService)
        {
            _context = context;
            _userManager = userManager;
            _orderService = orderService;
            _cartService = cartService;
            _shippingDetailsService = shippingDetailsService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.Orders.Include(o => o.User);
            return View(await productDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(c => c.OrderItems)
                .ThenInclude(p=>p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckoutView model)
        {
            var user = await _userManager.GetUserAsync(User);

            // Add Order and Shipping Details
            _orderService.AddOrders(user.Id, (double)model.TotalAmount);
            var order = _orderService.GetOrder(user.Id).LastOrDefault();

            _orderService.AddShippingDetails(user.Id, order.Id, model.ShippingDetail);

            // Add Order Items (assuming CartItems is included in the view model)
            var cart = _cartService.GetCart(user.Id);
            ICollection<CartItem> cartItems = cart.CartItems;
            _orderService.AddOrderItems(cartItems, order.Id, cart);
            _cartService.ClearCart(user.Id);

            return RedirectToAction("Index", "Home");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateOrderStatus(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            order.OrderStatus = "Delivered";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> Orders( string status)
        //{
        //    var shippingDetails = new ShippingDetail();
        //    var user = await _userManager.GetUserAsync(User);
        //    var orders = _orderService.GetOrder(user.Id).Where(o=>o.OrderStatus == status);
        //    foreach(var order in orders)
        //    {
        //        shippingDetails = _shippingDetailsService.GetShippingDetails(user.Id).FirstOrDefault(o=>o.OrderId ==order.Id);
        //    }
        //        var model = new OrderView
        //        {
        //            Orders = orders,
        //            ShippingDetails = shippingDetails
        //        };
        //    return View(model);
        //}

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
