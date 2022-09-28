using CleanArq.Application.Common.Interfaces.Services;

namespace CleanArq.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}