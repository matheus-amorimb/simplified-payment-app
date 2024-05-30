using System.ComponentModel.DataAnnotations;

namespace PicPayBackendChallenge.Dtos;

public class WalletRequestDto
{
    public string? FullName { get; set; }
    public string? Cpf { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public int WalletTypeId { get; set; }
}