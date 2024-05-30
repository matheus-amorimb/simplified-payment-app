namespace PicPayBackendChallenge.Services;

public interface IWalletConfirmartionEmailService
{
    void Publish(object data, string routingKey, string queue);
}