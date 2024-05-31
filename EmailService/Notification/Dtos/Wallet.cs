using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Notification.Dtos;

public class Wallet
{
    public Guid WalletId { get; set; }
    public string? FullName { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public double Balance { get; set; }
    public int WalletTypeId { get; set; }
}