using System.ComponentModel.DataAnnotations;

namespace Ecom.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Product>? Products { get; set; }
}
