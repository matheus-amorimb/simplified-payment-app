namespace SimplifiedPicPay.Dtos;

public class UserRegisterResponseDto
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    public int WalletTypeId { get; set; }
}