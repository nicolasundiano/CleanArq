namespace CleanArq.Infrastructure.Services.SendGrid;

public class SendGridSettings
{
    public const string SectionName = "SendGridSettings";
    public string ApiKey { get; set; } = null!;
    public string EmailFrom { get; set; } = null!;
    public string NameFrom { get; set; } = null!;
}
