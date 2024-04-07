namespace TravelMore.Infrastructure.Services.JwtTokenGenerators;

public record JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Key { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int ExpirationMinutes { get; }

    public JwtSettings(string key, string issuer, string audience, int expirationMinutes)
    {
        Key = key;
        Issuer = issuer;
        Audience = audience;
        ExpirationMinutes = expirationMinutes;
    }

}