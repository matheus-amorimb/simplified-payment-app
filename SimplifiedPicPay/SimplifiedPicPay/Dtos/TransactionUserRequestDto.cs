using System.ComponentModel.DataAnnotations;

namespace SimplifiedPicPay.Dtos;

public class TransactionUserRequestDto : IValidatableObject
{
    public float Value { get; set; }

    public Guid PayeeWalletId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult>  results = new List<ValidationResult>();
        
        if (!IsValidValue(Value))
        {
            results.Add(new ValidationResult("Transaction value must be greater than 0.1"));
        }

        return results;
    }

    private bool IsValidValue(float value)
    {
        return value > 0.1;
    }
}