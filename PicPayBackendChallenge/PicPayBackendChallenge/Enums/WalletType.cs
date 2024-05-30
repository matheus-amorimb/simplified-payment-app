using System.ComponentModel;

namespace PicPayBackendChallenge.Enums;

public enum WalletType
{
    [Description("User")]
    User,
    
    [Description("Merchant")]
    Merchant
}