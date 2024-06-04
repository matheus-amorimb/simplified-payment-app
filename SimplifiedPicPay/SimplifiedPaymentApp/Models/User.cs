using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace SimplifiedPicPay.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public override string Email { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    [JsonIgnore]
    public Wallet Wallet { get; set; }
    
    [Required]
    [Column("cpf")]
    // [Cpf]
    public string? Cpf { get; set; }
}

