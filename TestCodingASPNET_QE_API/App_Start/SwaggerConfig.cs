using System.Web.Http;
using WebActivatorEx;
using TestCodingASPNET_QE_API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace TestCodingASPNET_QE_API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c =>
              {
                  c.SingleApiVersion("v1", "Coding Test WEB API");
              })
              .EnableSwaggerUi(d => {
                  d.EnableApiKeySupport("Authorization", "header");
              });
        }
    }
}
