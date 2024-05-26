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
    [Column("cpf")]
    public string? Cpf { get; set; }
    
    [Required]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }
    
    [Required]
    [Column("wallet_type_id")]
    public int WalletTypeId { get; set; }
    
    [JsonIgnore]
    [ForeignKey("WalletTypeId")]
    public WalletType? WalletType { get; set; }
}