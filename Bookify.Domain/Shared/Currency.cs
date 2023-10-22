namespace Bookify.Domain.Shared;
public record Currency
{
    internal static readonly Currency None = new("");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Try = new("TRY");
    private Currency(string code) => Code = code;
    public string Code { get; init; }
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(x => x.Code == code) ??
            throw new ArgumentException($"Currency with code {code} not found");
    }
    public static readonly IReadOnlyCollection<Currency> All = new[] { Usd, Try };

}
