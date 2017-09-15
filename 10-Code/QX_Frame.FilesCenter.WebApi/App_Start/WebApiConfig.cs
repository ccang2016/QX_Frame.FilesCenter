using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QX_Frame.FilesCenter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            //跨域配置 //need reference from nuget
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //if config the global filter input there need not write the attributes
            config.Filters.Add(new App.WebApi.Filters.ExceptionAttribute_DG());
        }
    }
}
