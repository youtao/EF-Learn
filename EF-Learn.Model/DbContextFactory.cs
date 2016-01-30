using System.Data.Entity;
using System.Web;
using EF_Learn.ModelFactory;

namespace EF_Learn.Model
{
    public class DbContextFactory : IDbContextFactory
    {
        public DbContext GetContext(string assemblyName)
        {
            DbContext context = HttpContext.Current.Items[assemblyName] as DbContext;
            if (context == null)
            {
                context = new EFLearnContext();
                HttpContext.Current.Items.Add(assemblyName, context);//按程序集名称保存到Http缓存中
            }
            return context;         
        }
    }
}