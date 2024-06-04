using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimplifiedPicPay.Dtos;

public class TransactionRequestDto
{
    public float Value { get; set; }
    public Guid PayerWalletId { get; set; }

    public Guid PayeeWalletId { get; set; }
}