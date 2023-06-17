using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using StarWarsAPI.Entities;

namespace StarWarsAPI.Common;

public class JwtHelper
{
    private readonly JwtOptions _jwtOptions;
    
    public JwtHelper(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    
    public string GenerateJwt(User user)
    {
        var securityKey = _jwtOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Email,user.Email),
            new (JwtRegisteredClaimNames.Sub,user.Id.ToString()),
        };

        var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims,
            expires: DateTime.Now.AddDays(_jwtOptions.ExpirationDays), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}