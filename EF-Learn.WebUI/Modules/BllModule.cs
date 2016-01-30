using System.Reflection;
using Autofac;

namespace EF_Learn.WebUI.Modules
{
    public class BllModule:Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("EF-Learn.BLL"))
                      .Where(t => t.Name.EndsWith("Bll"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
            base.Load(builder);
        }

    }
}