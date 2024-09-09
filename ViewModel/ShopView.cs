using Ecom.Models;

namespace Ecom.ViewModel
{
    public class ShopView
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Guid? SelectedCategoryId { get; set; }
        public string? SortOrder { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public int? TotalProducts { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}
