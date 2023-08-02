using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Services.Test;

public class TokenManagerTest
{
    private IConfiguration configuration;
    private TokenManager tokenManager;

    [SetUp]
    public void Setup()
    {
        configuration = A.Fake<IConfiguration>();
        tokenManager = new TokenManager(configuration);
    }
    [Test]
    public void GenerateToken_WithCorrectPrivateKey_ReturnTokenWithClaimsUniqueNameAndRole()
    {
        var user = new User
        {
            Id = 1, Name = "John", Password = "none", RoleId = 1
        };
        var roleName = "User";

        var key = "MIIEpQIBAAKCAQEA2DzwpiJTEdh9hvI9QJIcyI92SBYW/KOlvsDAZsAdDUk03wtPPVFnHOVwm+rtM79hH59iq9PxeIdaJKHI5HRjFdRb/GABH5/Z6i8+Kq7pKDS2VERq3ACSMNuCJ65ke1bJDVEenKys+hMKDb2Ip3nusGF49YXXg6LbWm3ma7t\nJ5oeM8OoBXLH6uCKsVt9pOYvAZDB0TWXPRfCk5uskN3gxAN6cjBRhDsn4VrheodpgsZ+cVlV4zPEPjP/Ca/SY2kQjBCyIBmaPf/m535lDoMP2jnwqOpRhXcqrWTUCOij14T2P4b74sH9Rx9aRnA5/a4H9kEeklDKGHRWCrBK6tmUgeQIDAQABAo\nIBAQCiQjAt6cm9tV6UGUd/IWS51nTiKLk9ACtKFOcK8xOZuZoT2C+wilm+ZCh4xvMRBoWBrh7jYtlqIN6yaDgPvYnwgnY3zW5qZY+mW6bhbniEc/FxEBnDViZcxQpIbmL17ixVcs5usF/oEstTfiqByUwjTDDww2rxWw4QMDFcG6Cbey9HzgqrE\nt0w+Wu43j8AYZtKSxjW+h3tE0xgjUsv3DNQdYjBLZ0Z4/lMsB+MHEz6e/1/TJowyGNuwF4V0N51MrgXHg9pi7UjQZVJ8Gr765C9KsUErme2tZvos74KCtYzIMSOY1MVeslIPG1JJhoPQ3xRhjsGv1S5Vf0tl9RTfJUFAoGBANmTPftbMwp2ZxWu\nffCgn+xvEqBW9cXdGrL1+q7BJzaICX2EgX6zD8L2JKDLPQiIXj52Rx85lrmi6ZpXRM125ghgnxf/utr+u8GtCPxzi0DONr4LUmgS8D73+0zPYPFkEVnDIvVN3lPCys+jOw8baYnrPdJIq/T3TGhbngCUAUiDAoGBAP5tPuZAJLz7Spxwk3JDk3a\nRaPYAksZZT6rSZjUlJwezkFezPrTyTStSVl2ELZ1B4N527T073PSekZbkrhs+NrS3tUd3JOZ6yIQY+//rYeItgoVQt7gZ2wokRMb8x9bbKVmUc36y1WODzPlzgEgyKknhIUPXb995XUUAycHWTIpTAoGBAMA0YfH12/4nIOO3dQwoaX6tlK/Ogm\nmb7KUhxaWxflmfDXsznk32EztwxGTDhhROm6rkQ+oirrMpZuJwq5gyq/3ElWbXBBPIKsdqe+DAlcjXIub6C39jE5cc7IQrQwGcG/PG/c/kTT6DezS4h0jON4qeJMvqZPYPrREXtlneZc/PAoGBAJYT1uB2wbT//fjdpvvlxJxSFbnWiL2bfRTkW\nwnKSoWOc/xnbPvLWZ3OSceL6mQysfRH7pUKNMHOr050wgar4hUjsDjhnNCfaJwTKMLDE9AYzD7baCOejMgksLU38qFYUcHXgXEhCCJVYplaejcb8Dn4JGkiMYl+y3eiiWBfinKlAoGAZcs4bMUCpWVwZG1muxZj44BZw3Zt0vBSrVq0x0tugAzk\nhiE2F9KfjYsuPvGLBpUl2KPqtP8OuqcpJbhvzTujW/GMFgkTJ6ziJ2bJXFBWjacIXvXDeSxLJY5f/7Za154QtCS0Y1yZBW4sRNbxxyxPRXuAQF3gQ4LHffFE99cu3+0=";
        configuration["Jwt:PrivateKey"] = key;
        var token = tokenManager.GenerateToken(user, roleName);

        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadJwtToken(token);

        IList<Claim> claims = (IList<Claim>)decodedToken.Claims;
        Assert.That(claims[0].Type, Is.EqualTo(JwtRegisteredClaimNames.UniqueName));
        Assert.That(claims[0].Value, Is.EqualTo(user.Name));
        Assert.That(claims[1].Type, Is.EqualTo(ClaimTypes.Role));
        Assert.That(claims[1].Value, Is.EqualTo(roleName));
    }
}