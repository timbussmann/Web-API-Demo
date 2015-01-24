using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DemoApi
{
    using Thinktecture.IdentityModel.WebApi.Authentication.Handler;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
            authenticationConfiguration.AddBasicAuthentication((username, password) => username == "admin" && password == "1234");
            authenticationConfiguration.RequireSsl = false; // don't try this at home kids!
            GlobalConfiguration.Configure(config => config.MessageHandlers.Add(new AuthenticationHandler(authenticationConfiguration)));
        }
    }
}
