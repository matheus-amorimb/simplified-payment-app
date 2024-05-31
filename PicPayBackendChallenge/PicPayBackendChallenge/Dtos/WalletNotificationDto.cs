using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Dtos;

public class WalletNotificationDto
{
    public Wallet Wallet;

    public WalletNotificationDto(Wallet wallet)
    {
        this.Wallet = wallet;
    }
}