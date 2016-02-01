using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace EF_Learn.Common.Tools
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// 返回当前实体的表名
        /// </summary>
        /// <typeparam name="TClass">该实体类</typeparam>
        /// <param name="context">当前数据库上下文</param>
        /// <returns></returns>
        public static string GetTableName<TClass>(this DbContext context) where TClass : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            return objectContext.GetTableName<TClass>();
        }
        private static string GetTableName<TClass>(this ObjectContext context) where TClass : class
        {
            string sql = context.CreateObjectSet<TClass>().ToTraceString();//用于确定一条普通的查询语句(Good)            
            Regex regex = new Regex(@"FROM\s+(?<table>.+)\s+AS");//根据正则来提取查询语句中的表名
            Match match = regex.Match(sql);
            string table = match.Groups["table"].Value;
            return table;
        }
    }
}