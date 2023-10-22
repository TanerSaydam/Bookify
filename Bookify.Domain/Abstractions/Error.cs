using System.Diagnostics.CodeAnalysis;

namespace Bookify.Domain.Abstractions;
public sealed record Error(string Code, string Name)
{
    public static Error None = new(string.Empty, string.Empty);

    public static Error NullValue = new("Error.NullValue", "Null value was provided");
}
