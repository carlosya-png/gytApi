using System.IdentityModel.Tokens.Jwt;

public static class JwtHelper
{
    public static DateTime GetExpiration(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        return jwt.ValidTo; // UTC
    }
}