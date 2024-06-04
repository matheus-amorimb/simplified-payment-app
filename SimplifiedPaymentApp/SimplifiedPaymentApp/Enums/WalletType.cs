using System.ComponentModel;

namespace SimplifiedPicPay.Enums;

public enum WalletType
{
    [Description("User")]
    User,
    
    [Description("Merchant")]
    Merchant
}