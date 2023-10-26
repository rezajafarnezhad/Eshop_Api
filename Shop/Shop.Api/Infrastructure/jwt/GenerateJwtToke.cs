using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.UserAgg;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Infrastructure.jwt;

public class GenerateJwtToke
{
    public static string GenerateToke(UserDto user, IConfiguration configuration)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
        };
        //Hash SecurityKey
        var encryptorKey = Encoding.UTF8.GetBytes(configuration["Jwt:secKey"]);

        var encryptorCred = new EncryptingCredentials(new SymmetricSecurityKey(encryptorKey),
            SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:TokenKey"]));
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriber = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds,
            Expires = DateTime.Now.AddDays(7),
            Issuer = configuration["Jwt:JwtIssuer"],
            Audience = configuration["Jwt:JwtAudience"],
            EncryptingCredentials = encryptorCred,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriber);
        return tokenHandler.WriteToken(token);
    }


}