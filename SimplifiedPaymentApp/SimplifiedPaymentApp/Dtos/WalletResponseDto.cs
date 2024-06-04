namespace SimplifiedPicPay.Dtos;

public class WalletResponseDto
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public int WalletTypeId { get; set; }
    public float Balance { get; set; }
}