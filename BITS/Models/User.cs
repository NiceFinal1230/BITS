namespace BITS.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Roles { get; set; }
    public string Salt { get; set; }
    public string HashedPassword { get; set; }
    public string PhoneNumber { get; set; }
    public string StreetAddress { get; set; }
    public string PostCode { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }
    public string CardNumber { get; set; }
    public string CardOwner { get; set; }
    public string Expiry { get; set; }
    public string CVV { get; set; }
}
