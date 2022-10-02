namespace CleanArq.Application.Common.Interfaces.Services;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string emailTo, string message);
}
