using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using JsonPatch;
using JsonPatch.Formatting;
using JsonPatch.Paths.Resolvers;
using Microsoft.Owin.Security.OAuth;
using TestCodingASPNET_QE_API.Filters;

namespace TestCodingASPNET_QE_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.DependencyResolver = 

            // Web API configuration and services
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidateModelAttribute());

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new JsonPatchFormatter(new JsonPatchSettings
            {
                PathResolver = new FlexiblePathResolver()
            }));
        }
    }
}
