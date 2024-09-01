using System.ComponentModel.DataAnnotations;

namespace Ecom.Models;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double TotalPrice => Quantity * UnitPrice;
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
