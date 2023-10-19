namespace Services.Test;

public class TokenManagerTest
{
    private IConfiguration configuration = null!;
    private TokenManager tokenManager = null!;
    [SetUp]
    public void Setup()
    {
        configuration = A.Fake<IConfiguration>();
        tokenManager = new TokenManager(configuration);
    }
    [Test]
    public void GenerateToken_WithCorrectPrivateKey_ReturnTokenWithClaimsUniqueNameAndRole()
    {
        var user = DataGenerator.CreateTestUser(1, "John", "none");
        var roleName = "User";
        configuration["Jwt:PrivateKey"] = DataGenerator.GetPrivateKey();
        var token = tokenManager.GenerateToken(user.Name, roleName);
        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadJwtToken(token);

        IList<Claim> claims = (IList<Claim>)decodedToken.Claims;
        var nameClaim = claims[0];
        var roleClaim = claims[1];
        Assert.That(nameClaim.Type, Is.EqualTo(ClaimNames.UniqueName));
        Assert.That(nameClaim.Value, Is.EqualTo(user.Name));
        Assert.That(roleClaim.Type, Is.EqualTo(ClaimTypes.Role));
        Assert.That(roleClaim.Value, Is.EqualTo(roleName));
    }
}