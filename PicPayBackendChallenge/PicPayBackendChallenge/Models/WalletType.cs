using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PicPayBackendChallenge.Models;

[Table("wallet_type")]
public class WalletType
{
    [Key]
    [Required]
    [Column("wallet_type_id")]
    public int Id { get; set; }
    
    [Required]
    [Column("description")]
    [StringLength(50)]
    public string? Description { get; set; }
}