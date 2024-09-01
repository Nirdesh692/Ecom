using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecom.Models;
using Ecom.Data;

namespace Ecom.Controllers
{
    public class ShippingDetailsController : Controller
    {
        private readonly ProductDbContext _context;

        public ShippingDetailsController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: ShippingDetails
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.ShippingDetails.Include(s => s.Order);
            return View(await productDbContext.ToListAsync());
        }

        // GET: ShippingDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return View(shippingDetail);
        }

        // GET: ShippingDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: ShippingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ShippingDetail shippingDetail)
        {
            if (ModelState.IsValid)
            {
                shippingDetail.Id = Guid.NewGuid();
                _context.Add(shippingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shippingDetail.OrderId);
            return View(shippingDetail);
        }

        // GET: ShippingDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shippingDetail.OrderId);
            return View(shippingDetail);
        }

        // POST: ShippingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  ShippingDetail shippingDetail)
        {
            if (id != shippingDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingDetailExists(shippingDetail.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shippingDetail.OrderId);
            return View(shippingDetail);
        }

        // GET: ShippingDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return View(shippingDetail);
        }

        // POST: ShippingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shippingDetail = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetail != null)
            {
                _context.ShippingDetails.Remove(shippingDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingDetailExists(Guid id)
        {
            return _context.ShippingDetails.Any(e => e.Id == id);
        }
    }
}
