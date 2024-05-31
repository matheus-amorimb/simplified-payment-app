using Notification.Models;

namespace Notification.Services;

public interface IEmailService
{
    Task HandleEmailNotification(object notification);
    Task SendEmail(Email email);
}