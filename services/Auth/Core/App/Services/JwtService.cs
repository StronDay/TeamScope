using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Core.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Core.App.Services;

public class JwtService(IOptions<AuthSettings> options)
{
    public string GenerateToken(AccountModel accountModel)
    {
        var claims = new List<Claim>
        {
            new Claim("userName", accountModel.UserName),
            new Claim("id", accountModel.Id.ToString())    
        };

        var jwtToken = new JwtSecurityToken
        (
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(options.Value.SecretKey)
            ), 
            SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
