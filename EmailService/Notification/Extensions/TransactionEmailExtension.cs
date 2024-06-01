using System.Globalization;
using Notification.Dtos;

namespace Notification.Extensions;

public class TransactionEmailExtension
{
    public static string EmailTemplate(TransactionNotification transactionNotification)
    {
        var templatePath = @"/home/matheus/matheus-dev/code/projects/picpay-backend-challenge/EmailService/Notification/Templates/TransactionEmailTemplate.html";

        var templateContent = File.ReadAllText(templatePath);

        var emailContent = PopulateTemplate(templateContent, transactionNotification);

        return emailContent;
    }

    public static string PopulateTemplate(string template, TransactionNotification transactionNotification)
    {
        var utcTime = transactionNotification.Transaction.Timestamp;
        var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, brazilTimeZone);
        var cultureInfo = new CultureInfo("pt-BR");
        var formattedTime = localTime.ToString("dd/MMM/yyyy HH:mm:ss", cultureInfo);
        
        return template
            .Replace("{{PayeeFullName}}", transactionNotification.PayeeWallet.FullName)
            .Replace("{{TransactionValue}}", transactionNotification.Transaction.Value.ToString())
            .Replace("{{TransactionId}}", transactionNotification.Transaction.TransactionId.ToString())
            .Replace("{{TransactionDate}}", formattedTime);
    }
}