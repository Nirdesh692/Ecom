using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public double? GrandTotal => CartItems?.Sum(x => x.TotalPrice);
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        
    }
}
