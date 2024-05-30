using Notification.Models;

namespace Notification.Services;

public interface IEmailService
{
    Task SendEmail(Email email);
}