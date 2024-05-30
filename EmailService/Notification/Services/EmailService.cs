using System.Net;
using System.Net.Mail;
using System.Text;
using Notification.Models;

namespace Notification.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
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
        mailMessage.Subject = email.Subject;
        mailMessage.IsBodyHtml = true;
        
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>User Registered</h1>");
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<p>Thank you For Registering account</p>");
        mailMessage.Body = mailBody.ToString();

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