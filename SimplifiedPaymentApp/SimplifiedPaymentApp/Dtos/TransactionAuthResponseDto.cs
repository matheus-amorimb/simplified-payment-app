namespace SimplifiedPicPay.Dtos;

public class TransactionAuthResponseDto
{
    public string status { get; set; }
    public AuthorizationData data { get; set; }
}

public class AuthorizationData
{
    public bool Authorization { get; set; }
}