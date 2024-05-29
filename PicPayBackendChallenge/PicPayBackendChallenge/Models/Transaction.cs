using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PicPayBackendChallenge.Models;

[Table("transaction")]
public class Transaction
{
    [Key]
    [JsonIgnore]
    [Column("transaction_id")]
    public Guid TransactionId { get; set; }
    
    [Required]
    [Column("value")]
    public float Value { get; set;}
    
    [Required]
    [Column("payer_id")]
    public Guid PayerId { get; set; }    
    
    [Required]
    [Column("payee_id")]
    public Guid PayeeId { get; set; }

    [Required] 
    [JsonIgnore]
    [Column("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

}