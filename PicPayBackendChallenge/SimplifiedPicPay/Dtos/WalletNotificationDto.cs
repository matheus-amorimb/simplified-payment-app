using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Dtos;

public class WalletNotificationDto
{
    public Wallet Wallet;

    public WalletNotificationDto(Wallet wallet)
    {
        this.Wallet = wallet;
    }
}