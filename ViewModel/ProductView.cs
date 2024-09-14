using Ecom.Models;

namespace Ecom.ViewModel
{
    public class ProductView
    {
        public Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
