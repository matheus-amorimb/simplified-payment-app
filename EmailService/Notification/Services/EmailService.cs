using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using Notification.Dtos;
using Notification.Extensions;
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
        email.Content = TransactionEmailExtension.EmailTemplate(transactionNotification);
        return email;
    }    
    private Email CreateWalletConfirmationEmail(WalletNotification walletNotification)
    {
        Email email = new Email();
        email.ToEmail = walletNotification.Wallet.Email;
        email.Subject = ("Welcome to MatheusPay!");
        email.Content = WalletEmailExtension.EmailTemplate(walletNotification);
        return email;
    }

    public async Task SendEmail(Email email)
    {
        var client = CreateSmptClient();
        var mailMessage = BuildMailMessage(email);
        
        try
        {
            Console.WriteLine(mailMessage.From);
            Console.WriteLine(mailMessage.To);
            Console.WriteLine(mailMessage.Subject);
            await client.SendMailAsync(mailMessage);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
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
        Console.WriteLine(password);
        
        SmtpClient client = new SmtpClient(smtpServer, int.Parse(smtpPort));

        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(username, password);

        return client;
    }
}