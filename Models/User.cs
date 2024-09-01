using Microsoft.AspNetCore.Identity;

namespace Ecom.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Provience { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}
