using Notification.Dtos;

namespace Notification.Extensions;

public class WalletEmailExtension
{
    public static string EmailTemplate(WalletNotification walletNotification)
    {
        var templatePath = @"/home/matheus/matheus-dev/code/projects/picpay-backend-challenge/EmailService/Notification/Templates/WalletEmailTemplate.html";

        var templateContent = File.ReadAllText(templatePath);

        var emailContent = PopulateTemplate(templateContent, walletNotification);

        return emailContent;
    }

    public static string PopulateTemplate(string template, WalletNotification walletNotification)
    {

        return template
            .Replace("{{WalletId}}", walletNotification.Wallet.WalletId.ToString())
            .Replace("{{FullName}}", walletNotification.Wallet.FullName);
    }
}