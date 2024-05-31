using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Dtos;

public class TransactionNotificationDto
{
    public Transaction Transaction { get; set; }
    public Wallet PayerWallet { get; set; }
    public Wallet PayeeWallet { get; set; }
    
    public TransactionNotificationDto(Transaction transaction, Wallet payerWallet, Wallet payeeWallet)
    {
        Transaction = transaction;
        PayerWallet = payerWallet;
        PayeeWallet = payeeWallet;
    }
}