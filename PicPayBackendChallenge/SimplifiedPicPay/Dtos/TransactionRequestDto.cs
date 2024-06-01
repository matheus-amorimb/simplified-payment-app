using System.ComponentModel.DataAnnotations;

namespace SimplifiedPicPay.Dtos;

public class TransactionRequestDto : IValidatableObject
{
    public float Value { get; set; }

    public Guid PayerId { get; set; }

    public Guid PayeeId { get; set; }

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