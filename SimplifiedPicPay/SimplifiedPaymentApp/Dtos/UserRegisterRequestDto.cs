using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimplifiedPicPay.Dtos;

public class UserRegisterRequestDto : IValidatableObject
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [JsonIgnore]
    public string? Username { get; set; }
    
    [Required]
    public string? Cpf { get; set; }
    
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }
    
    [Required]
    public int WalletTypeId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (!IsWalletTypeValid(WalletTypeId))
        {
            results.Add(new ValidationResult("WalletType id must be 1 (User) or 2 (Merchant)"));
        }

        return results;
    }

    private bool IsWalletTypeValid(int walletTypeId)
    {
        return (walletTypeId == 1 || walletTypeId == 2);
    }
    
}