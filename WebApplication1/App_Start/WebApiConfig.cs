using System.Web.Http;
using WebService.App_Start;

namespace WebService
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                   name: "DefaultApi",
                      routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optimal }
                 );

        }

    }
}