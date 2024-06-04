namespace SimplifiedPicPay.Dtos;

public class TransactionResponseDto
{
    public Guid TransactionId { get; set; }
    
    public float Value { get; set; }

    public Guid PayerWalletId { get; set; }

    public Guid PayeeWalletId { get; set; }
    
    public DateTime Timestamp { get; set; } 
}