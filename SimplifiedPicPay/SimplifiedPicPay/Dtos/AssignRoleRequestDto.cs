using System.ComponentModel.DataAnnotations;

namespace SimplifiedPicPay.Dtos;

public class AssignRoleRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Role { get; set; }
}