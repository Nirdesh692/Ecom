using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsActive { get; set; }
    public Category? Category { get; set; }

}
