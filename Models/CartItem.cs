using Ecom.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models;

public class CartItem
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid CartId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    public double TotalPrice
    {
        get
        {
            return Quantity * UnitPrice;
        }
    }

    public Cart Cart { get; set; }
    public Product Product { get; set; }
}
