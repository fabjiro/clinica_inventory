using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Interface;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService : IJwtService
{
    private string _secretKey;
    private string _issuer;
    private string _audience;

    public JwtService(string secretKey, string issuer, string audience)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
    }

    public string GenerateToken(string userId, int time = 8)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(time), // Define la fecha de expiración del token
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = key
            }, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string? GetUserIdFromToken(string token)
{
    var tokenHandler = new JwtSecurityTokenHandler();

    // Decodificar el token sin validarlo
    var jwtToken = tokenHandler.ReadJwtToken(token);
    // Console.WriteLine(jwtToken.Claims.FirstOrDefault());

    // Buscar el claim de userId (o cualquier otro claim)
    return jwtToken.Claims.FirstOrDefault()?.Value;
}

}