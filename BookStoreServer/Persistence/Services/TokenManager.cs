using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreServer.Persistence.Services;

public class TokenManager : ITokenManager
{
    private readonly IConfiguration _configuration;

    public TokenManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user, string roleName)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(_configuration["Jwt:PrivateKey"]), out _);
        var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
        var jwtDate = DateTime.Now;
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.UniqueName, user.Name),
            new(ClaimTypes.Role, roleName)
        };
        var jwt = new JwtSecurityToken(claims: claims, notBefore: jwtDate, expires: jwtDate.AddMinutes(60), 
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}