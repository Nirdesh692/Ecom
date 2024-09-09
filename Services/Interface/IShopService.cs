using Ecom.Models;
using Ecom.ViewModel;

namespace Ecom.Services.Interface
{
    public interface IShopService
    {
        Task<ShopView> GetShopViewAsync(int? pageNumber, int? pageSize);

        Task<ShopView> GetSearchViewAsync(List<string>? categoryId, string searchTerm, string? sortorder, double? minPrice, double? maxPrice, int? pageNumber, int? pageSize);
        Task<List<Category>> GetDistinctCategoriesAsync();


    }
}
