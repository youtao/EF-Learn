using System.Data.Entity;

namespace EF_Learn.ModelFactory
{
    /// <summary>
    /// 模型上下文工厂
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <param name="assemblyName">程序集名</param>
        /// <returns></returns>
        DbContext GetContext(string assemblyName);
    }
}