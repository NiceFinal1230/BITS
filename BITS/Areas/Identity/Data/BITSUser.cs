using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BITS.Areas.Identity.Data;

public class BITSUser : IdentityUser
{
    public override string? UserName { get; set; } = string.Empty;
    
    public override string? Email { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Roles { get; set; } = string.Empty;
    public string? Salt { get; set; } = string.Empty;
    public string? HashedPassword { get; set; } = string.Empty;
    public override string? PhoneNumber { get; set; } = string.Empty;
    public string? StreetAddress { get; set; } = string.Empty;
    public string? PostCode { get; set; } = string.Empty;
    public string? Suburb { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public string? CardNumber { get; set; } = string.Empty;
    public string? CardOwner { get; set; } = string.Empty;
    public string? Expiry { get; set; } = string.Empty;
    public string? CVV { get; set; } = string.Empty;

    [ValidateNever]
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
}
