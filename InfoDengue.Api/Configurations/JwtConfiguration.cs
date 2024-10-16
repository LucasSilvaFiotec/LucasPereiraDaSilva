using InfoDengue.Services.Authorization;
using InfoDengue.Services.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InfoDengue.Services.Configurations
{
    /// <summary>
    /// Classe para configuração do JWT
    /// </summary>
    public class JwtConfiguration
    {        
        public static void AddJwt(WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(settings);

            var appSettings = settings.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            builder.Services.AddAuthentication(
                auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(auth =>
                {
                    auth.RequireHttpsMetadata = false;
                    auth.SaveToken = true;
                    auth.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddTransient(map => new JwtService(appSettings));
        }
    }
}
