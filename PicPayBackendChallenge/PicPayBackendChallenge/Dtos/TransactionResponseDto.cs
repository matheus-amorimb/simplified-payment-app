namespace PicPayBackendChallenge.Dtos;

public class TransactionResponseDto
{
    public Guid TransactionId { get; set; }
    
    public float Value { get; set; }

    public Guid PayerId { get; set; }

    public Guid PayeeId { get; set; }
    
    public DateTime Timestamp { get; set; } 
}