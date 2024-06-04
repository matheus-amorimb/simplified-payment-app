using System.ComponentModel.DataAnnotations;

namespace SimplifiedPicPay.Dtos;

public class CreateRoleRequestDto
{
    [Required]
    public string? RoleName { get; set; }
}