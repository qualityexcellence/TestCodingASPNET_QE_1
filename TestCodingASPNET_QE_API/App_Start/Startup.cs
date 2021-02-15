using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartupAttribute(typeof(TestCodingASPNET_QE_API.Startup))]
namespace TestCodingASPNET_QE_API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://qualityexcellence.info", //some string, normally web url,  
                        ValidAudience = "https://qualityexcellence.info",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MM22!DRJKO90@^DBP!XU1Q76D"))
                    }
                });
        }
    }
}