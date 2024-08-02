using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BITS.Areas.Identity.Data;

public class BITSUser : IdentityUser
{
    public override string? UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public override string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Length of name must between 3 to 100.")]
    public string Name { get; set; } = string.Empty;


    // The actual roles system handle by IdentityManager
    // This property is used to handle the display of view
    [Required(ErrorMessage = "Roles is required.")]
    public string Roles { get; set; } = string.Empty;


    public string? Salt { get; set; } = string.Empty;
    public string? HashedPassword { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string? Password{ get; set; } = string.Empty;
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
