using Ecom.Data;
using Ecom.Models;
using Ecom.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services
{
    public class ShippingDetailsSerivce : IShippingDetailsService
    {
        private readonly ProductDbContext _context;

        public ShippingDetailsSerivce(ProductDbContext context)
        {
            _context = context;
        }
        public ICollection<ShippingDetail> GetShippingDetails(string userId)
        {
            var shippingDetails = _context.ShippingDetails
                                              .Include(s => s.Order)                                               
                                              .Where(s => s.Order.UserId == userId) 
                                              .ToList(); 
            return shippingDetails;
        } 
    }
}
