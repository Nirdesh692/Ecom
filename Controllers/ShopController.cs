using Ecom.Models;
using Ecom.Services.Interface;
using Microsoft.AspNetCore.Mvc;

public class ShopController : Controller
{
    private readonly IShopService _shopService;

    public ShopController(IShopService shopService)
    {
        _shopService = shopService;
    }

    public async Task<IActionResult> Shop(int? pageNumber, int? pageSize)
    {
        var shopView = await _shopService.GetShopViewAsync(pageNumber, pageSize);
        return View(shopView);
    }
    public async Task<IActionResult> Search(string searchString, List<string> categories, string sortOrder, double? minPrice, double? maxPrice, int? pageNumber, int? pageSize)
    {
        var shopView = await _shopService.GetSearchViewAsync(categories, searchString, sortOrder, minPrice, maxPrice, pageNumber, pageSize);

        var distinctCategories = await _shopService.GetDistinctCategoriesAsync();

        ViewBag.Categories = distinctCategories;

        return View(shopView);
    }
}
