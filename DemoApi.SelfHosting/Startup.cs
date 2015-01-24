namespace DemoApi.SelfHosting
{
    using System.Web.Http;
    using DemoApi.Controllers;
    using Microsoft.Owin.Cors;
    using Newtonsoft.Json.Serialization;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            // we cannot use the configuration from the web host project because the cors library differ for webhost and owin
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();

            //appBuilder.UseWebApi(config);

            appBuilder
                .UseNinjectMiddleware(this.CreateKernel)
                .UseNinjectWebApi(config)
                .UseCors(new CorsOptions());
        }

        private IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ITaskList>().ToConstant(new TaskList()).InSingletonScope();

            return kernel;
        }
    }
}