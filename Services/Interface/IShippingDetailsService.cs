using Ecom.Models;

namespace Ecom.Services.Interface
{
    public interface IShippingDetailsService
    {
        ICollection<ShippingDetail> GetShippingDetails(string userId);
    }
}
