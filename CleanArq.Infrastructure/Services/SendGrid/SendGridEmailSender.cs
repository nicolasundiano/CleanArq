using CleanArq.Application.Common.Interfaces.Services;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Options;

namespace CleanArq.Infrastructure.Services.SendGrid;

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridSettings _settings;

    public SendGridEmailSender(IOptions<SendGridSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<bool> SendEmailAsync(string emailTo, string message)
    {
        var apiKey = _settings.ApiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_settings.EmailFrom, _settings.NameFrom);
        var subject = "Sending with SendGrid is Fun :)";
        var to = new EmailAddress(emailTo, "Destination User");
        var plainTextContent = message;
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }
}
