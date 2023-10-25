using Bookify.Application.Abstractions.Clock;

namespace Bookify.Infrastructure.Clock;

public sealed class DataTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}