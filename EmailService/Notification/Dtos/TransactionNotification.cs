namespace Notification.Dtos;

public class TransactionNotification
{
    public Transaction Transaction { get; set; }
    public Wallet PayerWallet { get; set; }
    public Wallet PayeeWallet { get; set; }
    
    public TransactionNotification(Transaction transaction, Wallet payerWallet, Wallet payeeWallet)
    {
        Transaction = transaction;
        PayerWallet = payerWallet;
        PayeeWallet = payeeWallet;
    }

}