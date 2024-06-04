using System.ComponentModel.DataAnnotations;

namespace SimplifiedPicPay.Validation;

public class CpfAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value.ToString().Length != 11)
        {
            return new ValidationResult("CPF must be 11 digits long.");
        }
        
        foreach (char letter in value.ToString())        
        {
            if (!char.IsDigit(letter))
            {
                return new ValidationResult("CPF must contain only digits.");
            }
        }

        string? cpf = value.ToString();
        
        int sum = 0;
        int x = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (cpf[i] - '0') * (10 - i);
        }

        int remainder = sum % 11;
        int firstCheckDigit = (remainder < 2) ? 0 : (11 - remainder);

        if (firstCheckDigit != (cpf[9] - '0'))
        {
            return new ValidationResult("CPF is invalid.");
        }

        sum = 0;
        for (int i = 1; i < 10; i++)
        {
            sum += (cpf[i] - '0') * (11 - i);
        }
        remainder = sum % 11;
        int secondCheckDigit = (remainder < 2) ? 0 : (11 - remainder);

        if (secondCheckDigit != (cpf[10] - '0'))
        {
            return new ValidationResult("CPF is invalid.");
        }
        
        return ValidationResult.Success;
    }
}