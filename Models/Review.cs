using Microsoft.AspNetCore.Identity;

namespace Ecom.Models;

public class Review
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
}
