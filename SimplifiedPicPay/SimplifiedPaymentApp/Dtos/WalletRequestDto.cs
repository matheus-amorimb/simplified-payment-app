using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimplifiedPicPay.Dtos;

public class WalletRequestDto : IValidatableObject
{
    [Required]
    public string? FullName { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [JsonIgnore]
    public Guid UserId { get; set; }
    
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