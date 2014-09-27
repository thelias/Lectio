using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;

namespace LectioServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(DefaultAuthenticationTypes.ExternalBearer)); // make sure this matches the auth type in 
            //config.Filters.Add(new NewRelicExceptionFilter());
            //config.Filters.Add(new RequireWebApiHttpsAttribute());
            //config.Filters.Add(new AuthorizeAttribute()); // this is not working, possibly becaues attribute routes

            config.MapHttpAttributeRoutes();

            //formatters.Remove(formatters.XmlFormatter);
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.EnableQuerySupport();

            config.EnableCors(new EnableCorsAttribute("https://localhost:57528,http://wekeep.azurewebsites.net", "*", "*"));
        }
    }
}
