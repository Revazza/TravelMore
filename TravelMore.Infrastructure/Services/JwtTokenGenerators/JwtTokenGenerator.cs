using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelMore.Application.Services;
using TravelMore.Domain.Users;

namespace TravelMore.Infrastructure.Services.JwtTokenGenerators;

public class JwtTokenGenerator(IOptions<JwtSettings> settings) : IJwtTokenGenerator
{
    private readonly JwtSettings _settings = settings.Value;

    public string Generate(User user)
    {
        var claims = GetClaims(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = GenerateJwtSecurityToken(claims, credentials);
        var tokenGenerator = new JwtSecurityTokenHandler();

        return tokenGenerator.WriteToken(token);
    }

    private static List<Claim> GetClaims(User user)
        =>
            [
                new(ClaimTypes.Email, user.Email)
            ];

    private JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims, SigningCredentials credentials)
        => new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),
            signingCredentials: credentials);

}
