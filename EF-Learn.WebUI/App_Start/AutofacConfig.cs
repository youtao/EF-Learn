using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using EF_Learn.WebUI.Modules;

namespace EF_Learn.WebUI
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new Autofac.ContainerBuilder();            

            builder.RegisterControllers(typeof(Global).Assembly).PropertiesAutowired();//mvc

            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());//api
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterModule(new DalModule());
            builder.RegisterModule(new BllModule());    
        
            var container = builder.Build();            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//mvc
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);//api
        }
    }
}  