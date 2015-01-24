using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoApi
{
    using System.Web.Http.Cors;
    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var corsEnabled = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsEnabled);

            // Web API configuration and services
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
