using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using Notification.Dtos;
using Notification.Models;

namespace Notification.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task HandleEmailNotification(object notification)
    {
        Email email = CreateEmailFactory(notification);
        await SendEmail(email);
    }
    private Email CreateEmailFactory(object notification)
    {
        if (notification is TransactionNotification transactionNotification)
        {
            return CreateTransactionConfirmationEmail(transactionNotification);
        }        
        if (notification is WalletNotification walletNotification)
        {
            Console.WriteLine("isWalletNotification");
            return CreateWalletConfirmationEmail(walletNotification);
        }
        
        return null;
    }

    private Email CreateTransactionConfirmationEmail(TransactionNotification transactionNotification)
    {
        Email email = new Email();
        email.ToEmail = transactionNotification.PayerWallet.Email;
        email.Subject = ("Your transaction was confirmed!");

        StringBuilder content = new StringBuilder();
        content.AppendLine("<h2><strong>Transaction confirmed</strong></h2>");
        content.AppendLine("<br/>");
        content.AppendLine($"<p>You made a transaction to:</p>");
        content.AppendLine($"<p><strong>{transactionNotification.PayeeWallet.FullName.ToUpper()}</strong></p>");
        content.AppendLine("<br/>");
        content.AppendLine($"<p>Transaction value:</p>");
        content.AppendLine($"<p>R$ {transactionNotification.Transaction.Value.ToString("F2")}</p>");
        content.AppendLine("<br/>");
        content.AppendLine("<br/>");
        content.AppendLine("<h3><strong>Transaction details</strong></h3>");
        content.AppendLine("<br/>");
        content.AppendLine("<p>Transaction Id</p>");
        content.AppendLine($"<p>{transactionNotification.Transaction.TransactionId.ToString()}</p>");
        content.AppendLine("<br/>");
        content.AppendLine("<p>Date and hour</p>");
        var cultureInfo = new CultureInfo("pt-BR");;
        var localTime = transactionNotification.Transaction.Timestamp.ToLocalTime();
        content.AppendLine($"<p>{localTime.ToString("dd/MMM/yyyy HH:mm:ss", cultureInfo)}</p>");

        email.Content = content.ToString();
        Console.WriteLine(email);
        return email;
    }    
    private Email CreateWalletConfirmationEmail(WalletNotification walletNotification)
    {
        Email email = new Email();
        email.ToEmail = walletNotification.Wallet.Email;
        email.Subject = ("Welcome to PicPay!");

        StringBuilder content = new StringBuilder();
        content.AppendLine("<h2><strong>Account created successfully!</strong></h2>");
        content.AppendLine("<br/>");
        content.AppendLine("<br/>");
        content.AppendLine($"<h3><strong>Account details:</strong></h3>");
        content.AppendLine("<br/>");
        content.AppendLine($"<p>Wallet id: {walletNotification.Wallet.WalletId}</p>");
        content.AppendLine($"<p>Name: {walletNotification.Wallet.FullName}</p>");

        email.Content = content.ToString();

        return email;
    }

    public async Task SendEmail(Email email)
    {
        var client = CreateSmptClient();
        var mailMessage = BuildMailMessage(email);
        
        try
        {
            await client.SendMailAsync(mailMessage);
            
        }
        catch (Exception e)
        {
            client.Dispose();
            mailMessage.Dispose();
        }
        
    }

    private MailMessage BuildMailMessage(Email email)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_configuration["EmailSettings:Username"] ?? string.Empty);
        mailMessage.To.Add(email.ToEmail);
        mailMessage.IsBodyHtml = true;
        mailMessage.Subject = email.Subject;
        mailMessage.Body = email.Content;

        return mailMessage;
    }

    private SmtpClient CreateSmptClient()
    {
        IConfigurationSection smtpSettings = _configuration.GetSection("EmailSettings");

        var smtpServer = smtpSettings["SmtpServer"];
        var smtpPort = smtpSettings["SmtpPort"];
        var senderEmail = smtpSettings["SenderEmail"];
        var username = smtpSettings["Username"];
        var password = smtpSettings["Password"];
        
        SmtpClient client = new SmtpClient(smtpServer, int.Parse(smtpPort));

        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(username, password);

        return client;
    }
}