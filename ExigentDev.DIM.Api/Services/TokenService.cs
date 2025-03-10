using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace ExigentDev.DIM.Api.Services
{
  public class TokenService : ITokenService
  {
    private readonly string _signingKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly SymmetricSecurityKey _key;

    public TokenService()
    {
      _signingKey =
        Environment.GetEnvironmentVariable("JWT_SIGNINGKEY")
        ?? throw new InvalidOperationException("JWT_SIGNINGKEY environment variable is not set.");

      _issuer =
        Environment.GetEnvironmentVariable("JWT_ISSUER")
        ?? throw new InvalidOperationException("JWT_ISSUER environment variable is not set.");

      _audience =
        Environment.GetEnvironmentVariable("JWT_AUDIENCE")
        ?? throw new InvalidOperationException("JWT_AUDIENCE environment variable is not set.");

      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
    }

    public string CreateToken(AppUser appUser)
    {
      var claims = new List<Claim>
      {
        new(ClaimTypes.Name, appUser.UserName!),
        new(JwtRegisteredClaimNames.Email, appUser.Email!),
        new(JwtRegisteredClaimNames.GivenName, appUser.UserName!),
      };

      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds,
        Issuer = _issuer,
        Audience = _audience,
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
