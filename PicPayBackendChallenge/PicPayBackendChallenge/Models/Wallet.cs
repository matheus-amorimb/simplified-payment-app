using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PicPayBackendChallenge.Validation;

namespace PicPayBackendChallenge.Models;

[Table("wallet")]
public class Wallet
{
    [Key]
    [Column("wallet_id")]
    public Guid WalletId { get; set; }
    
    [Required]
    [Column("full_name")]
    public string? FullName { get; set; }
    
    [Required]
    [Column("cpf")]
    // [Cpf]
    public string? Cpf { get; set; }
    
    [Required]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }
    
    [Column("balance")]
    public double Balance { get; set; } = 0.00;
    
    [Required]
    [Column("wallet_type_id")]
    public int WalletTypeId { get; set; }
    
    [JsonIgnore]
    [ForeignKey("WalletTypeId")]
    public WalletType? WalletType { get; set; }

    [NotMapped]
    public Boolean IsUser => this.WalletTypeId == 1;
}