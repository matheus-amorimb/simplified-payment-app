using System.ComponentModel.DataAnnotations;

namespace SimplifiedPicPay.Dtos;

public class UserLoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}