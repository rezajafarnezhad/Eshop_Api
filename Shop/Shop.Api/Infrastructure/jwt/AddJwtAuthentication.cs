using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shop.Api.Infrastructure.jwt;

public static class AddJwtAuthentication
{
    public static void AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = configuration["Jwt:JwtAudience"],
                ValidIssuer = configuration["Jwt:JwtIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:TokenKey"])),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:secKey"]))
            };
            option.SaveToken = true;
        });


    }
}