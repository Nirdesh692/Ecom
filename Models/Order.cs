using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalAmount { get; set; }
    public string OrderStatus { get; set; }
    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

}
