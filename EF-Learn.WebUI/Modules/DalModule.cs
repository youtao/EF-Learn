using System.Reflection;
using Autofac;

namespace EF_Learn.WebUI.Modules
{
    public class DalModule:Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("EF-Learn.DAL"))
                 .Where(t => t.Name.EndsWith("Dal"))
                 .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
