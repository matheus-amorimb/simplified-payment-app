using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Notification.Dtos;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public float Value { get; set;}
    public Guid PayerWalletId { get; set; }    
    public Guid PayeeWalletId { get; set; }
    public DateTime Timestamp { get; set; }
}