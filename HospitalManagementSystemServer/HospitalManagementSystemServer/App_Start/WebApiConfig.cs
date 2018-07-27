using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;



namespace HospitalManagementSystemServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //EnableCorsAttribute 
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            Controllers.UserLoginController userLController = new Controllers.UserLoginController();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
               // routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            //routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            defaults:  new { controller = "userLController" , id = RouteParameter.Optional }
            );
        }
    }
}
