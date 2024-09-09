namespace Ecom.Models;

public class ShippingDetail
{
    public Guid Id { get; set; }
    public Guid? OrderId { get; set; }
    public string ShippingAddress { get; set; }
    public string Provience { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public DateTime? ShippingDate { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public string ShippingStatus { get; set; }
    public Order? Order { get; set; }
}
