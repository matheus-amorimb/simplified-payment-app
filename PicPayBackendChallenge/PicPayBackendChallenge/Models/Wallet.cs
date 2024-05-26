using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PicPayBackendChallenge.Models;

[Table("wallet")]
public class Wallet
{
    [Key]
    [JsonIgnore]
    [Column("wallet_id")]
    public Guid WalletId { get; set; }
    
    [Required]
    [Column("full_name")]
    public string? FullName { get; set; }
    
    [Required]
    [EmailAddress]
    [Column("cpf")]
    public string? Cpf { get; set; }
    
    [Required]
    [Column("email")]
    public string? Email { get; set; }
    
    [Required]
    [Column("type")]
    public string? Type { get; set; }
}