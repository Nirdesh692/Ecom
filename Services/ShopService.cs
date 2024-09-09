using Ecom.Data;
using Ecom.Models;
using Ecom.Services.Interface;
using Ecom.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Ecom.Services
{
    public class ShopService : IShopService
    {
        private readonly ProductDbContext _context;

        public ShopService(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ShopView> GetShopViewAsync(int? pageNumber, int? pageSize)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new
                        {
                            Product = p,
                            Category = c
                        };
            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 12;

            var totalProducts = await query.CountAsync();

            var productList = await query
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .Select(x => x.Product)
                .ToListAsync();

            return new ShopView
            {
                Products = productList,
                PageNumber = currentPage,
                PageSize = currentPageSize,
                TotalProducts = totalProducts,
            };

        }

        public async Task<ShopView> GetSearchViewAsync(List<string>? categories, string searchString, string? sortOrder, double? minPrice, double? maxPrice, int? pageNumber, int? pageSize)
        {
            // Create the base query joining Categories and Products
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new
                        {
                            Product = p,
                            Category = c
                        };

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Product.Name.Contains(searchString) ||
                                         x.Product.Description.Contains(searchString));
            }

            if (categories != null && categories.Count > 0)
            {
                query = query.Where(x => categories.Contains(x.Category.Name));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Product.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.Product.Price <= maxPrice.Value);
            }

            // Sorting logic
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(x => x.Product.Name);
                    break;
                case "lowtohigh":
                    query = query.OrderBy(x => x.Product.Price);
                    break;
                case "hightolow":
                    query = query.OrderByDescending(x => x.Product.Price);
                    break;
                default:
                    query = query.OrderBy(x => x.Product.Name);
                    break;
            }

            // Pagination
            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 12;

            var totalProducts = await query.CountAsync();

            var productList = await query
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .Select(x => x.Product)
                .ToListAsync();

            var distinctCategories = await _context.Categories
                .Select(c => new Category { Id = c.Id, Name = c.Name })
                .Distinct()
                .ToListAsync();

            return new ShopView
            {
                Products = productList,
                Categories = distinctCategories,
                SortOrder = sortOrder,
                PageNumber = currentPage,
                PageSize = currentPageSize,
                SearchTerm = searchString,
                TotalProducts = totalProducts,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };
        }

        public async Task<List<Category>> GetDistinctCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new Category { Id = c.Id, Name = c.Name })
                .Distinct()
                .ToListAsync();
        }
    }
}
