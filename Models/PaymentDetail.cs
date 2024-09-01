namespace Ecom.Models;

public class PaymentDetail
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public double PaymentAmount { get; set; }
    public string PaymentStatus { get; set; }
    public string PaymentMethod { get; set; }
    public Order Order { get; set; }
}
